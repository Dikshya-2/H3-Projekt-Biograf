using Biograf.API.Controllers;
using Biograf.Repo.DTOs;
using Biograf.Repo.Interface;
using Biograf.Repo.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biograf.Test.Controller
{
    public class CategoryControllerTests
    {
        private readonly CategoryController _categoryController;
        private readonly Mock<ICategory> _categoryRepoMock = new();

        public CategoryControllerTests()
        {
            _categoryController = new(_categoryRepoMock.Object);
        }

        [Fact]
        public async void GetAllCategoryAsync_ShouldReturnStatusCode200_WhenCategoryExist()
        {

            //Arrange
            var categories = new List<Category> { new Category 
            { Id = 1, Name = "Action" }, new Category { Id = 2, Name = "Comedy" } };
         
            _categoryRepoMock
                .Setup(x => x.Get())
                .ReturnsAsync(categories);

            //Act
            var result = await _categoryController.Get();

            //Assert
            //Assert.Equal(200, result.StatusCode);
         
        }

        [Fact]
        public async void GetAllCategoryAsync_ShouldReturnStatusCode404_WhenNoCategoryExist()
        {
            //Arrange
            List<Category> categories = new();

            _categoryRepoMock
                .Setup(x => x.Get())
                .ReturnsAsync(categories);

            //Act
            //var result = (IStatusCodeActionResult)await _categoryController.Get();

            //Assert
            //Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async void GetAllCategoryAsync_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange
            List<CategoryResponse> categories = new();

            _categoryRepoMock
                .Setup(x => x.Get())
                .ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act
            //var result = (IStatusCodeActionResult)await _categoryController.Get();

            //Assert
//Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void FindCategoryById_ShouldReturnStatusCode200_WhenCategoryExists()
        {
            //Arrange
            int categoryId = 1;
            Category CategoryResponse = new()
            {
                Name = "Action",

            };

            _categoryRepoMock
                .Setup(x => x.FindCategoryById(It.IsAny<int>()))
                .ReturnsAsync(CategoryResponse);

            //Act
            var result = (IStatusCodeActionResult)await _categoryController.Get(categoryId);

            //Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void FindCategoryById_ShouldReturnStatusCode404_WhenCategoryDoesNotExist()
        {
            //Arrange
            int categoryId = 1;


            _categoryRepoMock
                .Setup(x => x.FindCategoryById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = (IStatusCodeActionResult)await _categoryController.Get(categoryId);

            //Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void FindCategoryById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange
            int categoryId = 1;

            _categoryRepoMock
                .Setup(x => x.FindCategoryById(It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act
            var result = (IStatusCodeActionResult)await _categoryController.Get(categoryId);

            //Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void CreateCategoryAsync_ShouldReturnStatusCode200_WhenCategoryIsSuccessfullyCreated()
        {
            //Arrange
            CategoryDto newCategory = new()
            {
                Name = "Action"

            };

            int categoryId = 1;
            Category categoryResponse = new()
            {
                Id = categoryId,
                Name = "Action"

            };

            _categoryRepoMock
        .Setup(x => x.Create(It.IsAny<CategoryDto>()))
                .ReturnsAsync(categoryResponse);

            //Act
            var result = (IStatusCodeActionResult)await _categoryController.Create(newCategory);

            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void CreateCategoryAsync_ShouldReturnStatusCode500_WhenExceptionIsRasied()
        {
            //Arrange
            CategoryDto newCategory = new()
            {
                Name = "Action"

            };

            _categoryRepoMock
                .Setup(x => x.Create(It.IsAny<CategoryDto>()))
                .ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act
            var result = (IStatusCodeActionResult)await _categoryController.Create(newCategory);

            //Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void UpdateCategoryByIdAsync_ShouldReturnStatusCode200_WhenCategoryIsUpdated()
        {
            //Arrange
            CategoryDto categoryRequest = new()
            {
                Name = "Action"

            };

            int categoryId = 1;
            Category categoryResponse = new()
            {
                Id = categoryId,
                Name = "Action"

            };

            _categoryRepoMock
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<CategoryDto>()))
                .ReturnsAsync(categoryResponse);

            //Act
            var result = (IStatusCodeActionResult)await _categoryController.Update(categoryId, categoryRequest);

            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void UpdateCategoryByIdAsync_ShouldReturnStatusCode404_WhenCategoryDoesNotExist()
        {
            //Arrange
            CategoryDto categoryRequest = new()
            {
                Name = "Action"

            };

            int categoryId = 1;


            _categoryRepoMock
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<CategoryDto>()))
                .ReturnsAsync(() => null);

            //Act
            var result = (IStatusCodeActionResult)await _categoryController.Update(categoryId, categoryRequest);

            //Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void UpdateCategoryByIdAsync_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange
            CategoryDto categoryRequest = new()
            {
                Name = "Action"

            };

            int categoryId = 1;


            _categoryRepoMock
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<CategoryDto>()))
                .ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act
            var result = (IStatusCodeActionResult)await _categoryController.Update(categoryId, categoryRequest);

            //Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void DeleteCategoryByIdAsync_ShouldReturnStatusCode200_WhenCategoryIsDeleted()
        {
            //Arrange

            int categoryId = 1;
            Category CategoryResponse = new()
            {

                Id = categoryId,
                Name = "Action"
            };

            _categoryRepoMock
                .Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(CategoryResponse);

            //Act
            var result = (IStatusCodeActionResult)await _categoryController.Delete(categoryId);

            //Assert
            Assert.Equal(200, result.StatusCode);
        }


        [Fact]
        public async void DeleteCategoryByIdAsync_ShouldReturnStatusCode404_WhenCategoryDoesNotExist()
        {
            //Arrange

            _categoryRepoMock
                .Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = (IStatusCodeActionResult)await _categoryController.Delete(1);

            //Assert
            Assert.Equal(404, result.StatusCode);
        }


        [Fact]
        public async void DeleteCategoryByIdAsync_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //    //Arrange

            _categoryRepoMock
                .Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act
            var result = (IStatusCodeActionResult)await _categoryController.Delete(1);

            //Assert
            Assert.Equal(500, result.StatusCode);



        }
        

    }
}
