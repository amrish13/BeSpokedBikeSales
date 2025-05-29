using BeSpokedBikeSales.Data;
using BeSpokedBikeSales.Interface;
using BeSpokedBikeSales.Models;
using BeSpokedBikeSales.Services;
using Moq;

namespace BeSpokedBikeSalesTests
{
    public class SalesPersonTests
    {

        [Fact]
        public void GetListOfSalesPerson()
        {
            List<SalesPerson> salesList = new List<SalesPerson>();
            salesList.Add(new SalesPerson { SalesPersonId = 1, FirstName = "First", LastName = "Name", Address = "Address", Manager = "Manager", Phone = "123", StartDate = DateTime.UtcNow });
            salesList.Add(new SalesPerson { SalesPersonId = 2, FirstName = "Second", LastName = "Name", Address = "Address", Manager = "Manager", Phone = "123", StartDate = DateTime.UtcNow });

            var salesPersonService = new Mock<ISalesPersonService>();
            salesPersonService.Setup(service => service.GetListOfSalesPeople()).Returns(salesList);
            var people =salesPersonService.Object.GetListOfSalesPeople();

            Assert.True(people.Any());
            Assert.True(people.Count() == 2);
            Assert.True(people.FirstOrDefault(x => x.FirstName == "Second").SalesPersonId == 2);

        }

        [Fact]
        public void GetSalesPersonById()
        {
            List<SalesPerson> salesList = new List<SalesPerson>();
            salesList.Add(new SalesPerson { SalesPersonId = 1, FirstName = "First", LastName = "Name", Address = "Address", Manager = "Manager", Phone = "123", StartDate = DateTime.UtcNow });
            salesList.Add(new SalesPerson { SalesPersonId = 2, FirstName = "Second", LastName = "Name", Address = "Address", Manager = "Manager", Phone = "123", StartDate = DateTime.UtcNow });

            var salesPersonService = new Mock<ISalesPersonService>();
            salesPersonService.Setup(service => service.GetSalesPersonById(It.IsAny<int>())).Returns(salesList[0]);
            var people = salesPersonService.Object.GetSalesPersonById(1);

            Assert.True(people != null);
            Assert.True(people.FirstName == "First");
        }

        [Fact]
        public void CreateSalesPerson()
        {
            var salesPersonService = new Mock<ISalesPersonService>();
            salesPersonService.Setup(service => service.CreateSalesPerson(It.IsAny<SalesPerson>()));
            var created = salesPersonService.Object.CreateSalesPerson(
                new SalesPerson() { 
                    FirstName = "New",
                    LastName = " Person",
                    SalesPersonId = 3,
                    StartDate = DateTime.UtcNow
                });

        }

        [Fact]
        public void CreateSalesPersonFail()
        {
            var salesPersonService = new Mock<ISalesPersonService>();
            salesPersonService.Setup(service => service.CreateSalesPerson(It.IsAny<SalesPerson>()));
            var created = salesPersonService.Object.CreateSalesPerson(null);

        }

    }
}