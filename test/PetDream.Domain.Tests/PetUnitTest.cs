using System;
using FluentAssertions;
using PetDream.Domain.Entities;
using Xunit;

namespace PetDream.Domain.Tests
{
    public class PetUnitTest
    {
        [Fact]
        [Trait("Domain", "PetEntity")]
        public void CreatePet_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Pet(1, "Duke", "Dalmatian", "White", "Male", DateTime.Now);
            action.Should().NotThrow<PetDream.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact]
        [Trait("Domain", "PetEntity")]
        public void CreatePet_WithInvalidName_DomainExceptionEmptyName()
        {
            Action action = () => new Pet(1, "", "Dalmatian", "White", "Male", DateTime.Now);
            action.Should().Throw<PetDream.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact]
        [Trait("Domain", "PetEntity")]
        public void CreatePet_WithInvalidName_DomainExceptionNameIsToShort()
        {
            Action action = () => new Pet(1, "D", "Dalmatian", "White", "Male", DateTime.Now);
            action.Should().Throw<PetDream.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact]
        [Trait("Domain", "PetEntity")]
        public void CreatePet_WithInvalidBreed_DomainExceptionEmptyBreed()
        {
            Action action = () => new Pet(1, "Duke", "", "White", "Male", DateTime.Now);
            action.Should().Throw<PetDream.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact]
        [Trait("Domain", "PetEntity")]
        public void CreatePet_WithInvalidBreed_DomainExceptionBreedIsToShort()
        {
            Action action = () => new Pet(1, "Duke", "D", "White", "Male", DateTime.Now);
            action.Should().Throw<PetDream.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact]
        [Trait("Domain", "PetEntity")]
        public void CreatePet_WithInvalidColor_DomainExceptionEmptyColor()
        {
            Action action = () => new Pet(1, "Duke", "Dalmatian", "", "Male", DateTime.Now);
            action.Should().Throw<PetDream.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact]
        [Trait("Domain", "PetEntity")]
        public void CreatePet_WithInvalidColor_DomainExceptionColorIsToShort()
        {
            Action action = () => new Pet(1, "Duke", "Dalmatian", "Bl", "Male", DateTime.Now);
            action.Should().Throw<PetDream.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact]
        [Trait("Domain", "PetEntity")]
        public void CreatePet_WithInvalidGender_DomainExceptionEmptyGender()
        {
            Action action = () => new Pet(1, "Duke", "Dalmatian", "White", "", DateTime.Now);
            action.Should().Throw<PetDream.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact]
        [Trait("Domain", "PetEntity")]
        public void CreatePet_WithInvalidId_DomainExceptionNegativeId()
        {
            Action action = () => new Pet(-1, "Duke", "Dalmatian", "White", "", DateTime.Now);
            action.Should().Throw<PetDream.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact]
        [Trait("Domain", "PetEntity")]
        public void DeletePet_ChangeStatusValueToFalse()
        {
            Pet pet = new Pet(1, "Duke", "Dalmatian", "White", "Male", DateTime.Now);
            pet.Delete();
            Assert.False(pet.Status);
        }

        [Fact]
        [Trait("Domain", "PetEntity")]
        public void UpdatePet_WithValidParameters_ChangePetInformations()
        {
            Pet pet = new Pet(1, "Duke", "Dalmatian", "White", "Male", DateTime.Now);
            pet.Update("Duna", "Dalmatian", "White", "Female", DateTime.Now, true, 1);
            Assert.True(pet.Name == "Duna" && pet.Gender == "Female" && pet.PetOwnerId == 1);
        }

        [Fact]
        [Trait("Domain", "PetEntity")]
        public void UpdatePet_WithInvalidParameters_ChangePetInformations()
        {
            Pet pet = new Pet(1, "Duke", "Dalmatian", "White", "Male", DateTime.Now);
            Action action = () => pet.Update("D", "Dalmatian", "", "Female", DateTime.Now, true, 1);
            action.Should().Throw<PetDream.Domain.Validation.DomainExceptionValidation>();
        }

    }
}