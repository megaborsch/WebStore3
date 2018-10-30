using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStore.Controllers;
using WebStore.Interfaces.Api;

namespace WebStore.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        private HomeController _controller;
        [TestInitialize]
        public void SetupTest()
        {
            //var mockService = new Mock<IValuesService>();
            //mockService.Setup(c => c.GetAsync()).ReturnsAsync(new List<string> {"1", "2" });
            _controller = new HomeController();
        }
        [TestMethod]
        public async Task Index_Method_Returns_View()
        {
            // Arrange and act
            var result = await _controller.Index();
            // Assert
            Xunit.Assert.IsType<ViewResult>(result);
        }        [TestMethod]
        public void ContactUs_Returns_View()
        {
            var result = _controller.ContactUs();
            Xunit.Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void ErrorStatus_404_Redirects_to_NotFound()
        {
            var result = _controller.ErrorStatus("404");
            var redirectToActionResult = Xunit.Assert.IsType<RedirectToActionResult>(result);
            Xunit.Assert.Null(redirectToActionResult.ControllerName);
            Xunit.Assert.Equal("NotFound", redirectToActionResult.ActionName);
        }

        [TestMethod]
        public void ErrorStatus_Antother_Returns_Content_Result()
        {
            var result = _controller.ErrorStatus("500");
            var contentResult = Xunit.Assert.IsType<ContentResult>(result);
            Xunit.Assert.Equal("Статуcный код ошибки: 500", contentResult.Content);
        }


        [TestMethod]
        public void Checkout_Returns_View()
        {
            var result = _controller.Checkout();
            Xunit.Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void BlogSingle_Returns_View()
        {
            var result = _controller.BlogSingle();
            Xunit.Assert.IsType<ViewResult>(result);
        }


        [TestMethod]
        public void Blog_Returns_View()
        {
            var result = _controller.Blog();
            Xunit.Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void Error_Returns_View()
        {
            var result = _controller.Error();
            Xunit.Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void NotFound_Returns_View()
        {
            var result = _controller.NotFound();
            Xunit.Assert.IsType<ViewResult>(result);
        }
    }
}
