//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace BenefactsTests
//{
//    class TestWithMockRepo
//    {
//        [Fact]
//        public async Task Index_ReturnsAViewResult_WithAListOfBrainstormSessions()
//        {
//            // Arrange
//            var mockRepo = new Mock<IBrainstormSessionRepository>();
//            mockRepo.Setup(repo => repo.ListAsync())
//                .ReturnsAsync(GetTestSessions());
//            var controller = new HomeController(mockRepo.Object);

//            // Act
//            var result = await controller.Index();

//            // Assert
//            var viewResult = Assert.IsType<ViewResult>(result);
//            var model = Assert.IsAssignableFrom<IEnumerable<StormSessionViewModel>>(
//                viewResult.ViewData.Model);
//            Assert.Equal(2, model.Count());
//        }
//    }
//}
