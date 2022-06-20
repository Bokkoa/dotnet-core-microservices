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

            
        }

        [Fact]
        public void GetBooks(){
            // which method is responsible for book db query?

            // 1. simulate(mock) the entity framework core instance

            var mockContext =  new Mock<BookDbContext>();

            // 2. simulate(mock) IMapper
            var mockMapper = new Mock<IMapper>();

            // 3. Instance the handler class and give it the mocks\
            Query.Handler handler = new Query.Handler(mockMapper.Object, mockContext.Object);

        }
    }
}