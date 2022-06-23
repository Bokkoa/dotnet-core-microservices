using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;
using ShopServices.Api.Book.Application;
using ShopServices.Api.Book.Model;
using ShopServices.Api.Book.Persistence;
using Xunit;

namespace ShopServices.Api.Book.Tests
{
    public class BookServiceTest
    {

        private IEnumerable<BookMaterial> GetTestData(){
            A.Configure<BookMaterial>()
                    .Fill(x => x.Title).AsArticleTitle()
                    .Fill(x => x.BookMaterialId, () => { return Guid.NewGuid(); } );

            var list = A.ListOf<BookMaterial>(30);

            list[0].BookMaterialId = Guid.Empty;

            return list;
        }


        private Mock<BookDbContext> CreateContext(){
            var testData = GetTestData().AsQueryable();

            var dbSet = new Mock<DbSet<BookMaterial>>();

            // Mapping the dbset
            dbSet.As<IQueryable<BookMaterial>>().Setup(x => x.Provider).Returns(testData.Provider);
            dbSet.As<IQueryable<BookMaterial>>().Setup(x => x.Expression).Returns(testData.Expression);
            dbSet.As<IQueryable<BookMaterial>>().Setup(x => x.ElementType).Returns(testData.ElementType);
            dbSet.As<IQueryable<BookMaterial>>().Setup(x => x.GetEnumerator()).Returns(testData.GetEnumerator());

            dbSet.As<IAsyncEnumerable<BookMaterial>>()
                                        .Setup(x => x.GetAsyncEnumerator( new System.Threading.CancellationToken() ))
                                        .Returns( new AsyncEnumerator<BookMaterial>(testData.GetEnumerator()));

            // Adding the provider that allow us make a filter query
            dbSet.As<IQueryable<BookMaterial>>()
                  .Setup( x => x.Provider).Returns( new AsyncQueryProvider<BookMaterial>(testData.Provider));

            var context = new Mock<BookDbContext>();

            context.Setup( x => x.BookMaterial).Returns(dbSet.Object);

            return context;
            
        }
        
        [Fact]
        public async void GetBookById(){

            var mockContext = CreateContext();

            var mapConfig =  new MapperConfiguration(cfg => {
                cfg.AddProfile( new MappingTest());
            });

            var mapper = mapConfig.CreateMapper();

            var request = new QueryFilter.UniqueBook();

            request.BookId = Guid.Empty;

            var handler = new QueryFilter.Handler(mapper, mockContext.Object);

            var book = await handler.Handle(request, new System.Threading.CancellationToken());

            Assert.NotNull(book);
            Assert.True(book.BookMaterialId == Guid.Empty);

        }


        [Fact]
        public async void GetBooks(){

            System.Diagnostics.Debugger.Launch();

            // which method is responsible for book db query?

            // 1. simulate(mock) the entity framework core instance
            var mockContext =  CreateContext();

            // 2. simulate(mock) IMapper
            var mapConfig = new MapperConfiguration( cfg => {
                cfg.AddProfile( new MappingTest());
            });


            var mockMapper = mapConfig.CreateMapper();

            // 3. Instance the handler class and give it the mocks\
            Query.Handler handler = new Query.Handler(mockMapper, mockContext.Object);

            Query.Execute request = new Query.Execute();

            // getting books
            var list = await handler.Handle(request, new System.Threading.CancellationToken());

            // testing
            Assert.True(list.Any());

        }

        [Fact]
        public async void StoreBook(){
            var options = new DbContextOptionsBuilder<BookDbContext>()
                                .UseInMemoryDatabase(databaseName: "BookTest")
                                .Options;

            var context = new BookDbContext(options);

            var request = new NewBook.Execute();
            request.Title = "Book microservices";
            request.AuthorBook = Guid.Empty;
            request.PublishDate = DateTime.Now;


            var handler = new NewBook.Handler(context);

            // if it fails the exception returns a null
            var book = await handler.Handle(request, new System.Threading.CancellationToken() );

            Assert.True( book != null);
        }
    }
}