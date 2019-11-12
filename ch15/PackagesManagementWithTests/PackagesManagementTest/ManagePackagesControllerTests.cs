using System;
using Xunit;
using PackagesManagement.Controllers;
using PackagesManagement.Models.Packages;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DDD.ApplicationLayer;
using PackagesManagement.Commands;
using System.Linq;

namespace PackagesManagementTest
{
    public class ManagePackagesControllerTests
    {
        [Fact]
        public async Task DeletePostValidationFailedTest()
        {
        var controller = new ManagePackagesController();
        controller.ModelState
            .AddModelError("Name", "fake error");
            var vm = new PackageFullEditViewModel();
            var result = await controller.Edit(vm, null);
Assert.IsType<ViewResult>(result);
Assert.Equal(vm, (result as ViewResult).Model);
        }
        [Fact]
        public async Task DeletePostSuccessTest()
        {
            var controller = new ManagePackagesController();
            var commandDependency =
                new Mock<ICommandHandler<UpdatePackageCommand>>();
            commandDependency
                .Setup(m => m.HandleAsync(It.IsAny<UpdatePackageCommand>()))
                .Returns(Task.CompletedTask);
            var vm = new PackageFullEditViewModel();

            var result = await controller.Edit(vm, 
                commandDependency.Object);
            commandDependency.Verify(m => m.HandleAsync(
                It.IsAny<UpdatePackageCommand>()), 
                Times.Once);
            Assert.IsType<RedirectToActionResult>(result);
            var redirectResult = result as RedirectToActionResult;
            Assert.Equal(nameof(ManagePackagesController.Index), 
                redirectResult.ActionName);
            Assert.Null(redirectResult.ControllerName);
        }
    }
}
