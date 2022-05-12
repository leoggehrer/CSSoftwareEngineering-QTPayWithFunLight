using Microsoft.VisualStudio.TestTools.UnitTesting;
using QTPayWithFunLight.Logic.Models;
using System;
using System.Threading.Tasks;

namespace QTPayWithFunLight.Logic.UnitTest
{
    [TestClass]
    public class Payment2UnitTest : DataAccessUnitTest<Logic.Models.Payment>
    {
        public override IDataAccess<Logic.Models.Payment> CreateDataAccess()
        {
            return Logic.Factory.CreatePayments();
        }
        [TestMethod]
        public async Task Create_DateIsNotSet_DateMustBeNow()
        {
            var now = DateTime.Now;
            var model = new Payment
            {
                CreditCardNumber = "5012562444244093",
                Amount = 100m,
            };

            var expected = now;
            var actualModel = await Create_AccessModel_AndCheck(model);

            Assert.AreEqual((expected - now).TotalSeconds, (actualModel.Date - now).TotalSeconds, 5.0);
        }
        [TestMethod]
        public async Task Create_DateIsSetWithNow_DateMustBeNow()
        {
            var now = DateTime.Now;
            var model = new Payment
            {
                Date = now,
                CreditCardNumber = "5012562444244093",
                Amount = 100m,
            };

            var expected = now;
            var actualModel = await Create_AccessModel_AndCheck(model);

            Assert.AreEqual((expected - now).TotalSeconds, (actualModel.Date - now).TotalSeconds, 5.0);
        }
        [TestMethod]
        public async Task Create_DateIsSetWithFuture_DateMustBeNow()
        {
            var now = DateTime.Now;
            var model = new Payment
            {
                Date = now.AddHours(1),
                CreditCardNumber = "5012562444244093",
                Amount = 100m,
            };

            var expected = now;
            var actualModel = await Create_AccessModel_AndCheck(model);

            Assert.AreEqual((expected - now).TotalSeconds, (actualModel.Date - now).TotalSeconds, 5.0);
        }
        [TestMethod]
        public async Task Update_DateIsChanged_DateMustBeUnchanged()
        {
            var now = DateTime.Now;
            var model = new Payment
            {
                CreditCardNumber = "5012562444244093",
                Amount = 100m,
            };
            var changedModel = new Payment
            {
                Date = now.AddHours(1),
                CreditCardNumber = "5012562444244093",
                Amount = 100m,
            };

            var expected = now;
            var insertModel = await Create_AccessModel_AndCheck(model);
            using var updateDataAccess = CreateDataAccess();
            using var updateDataAccessAfter = CreateDataAccess();

            var updateModel = await updateDataAccess.GetByIdAsync(insertModel.Id);

            Assert.IsNotNull(updateModel);
            updateModel.CopyFrom(changedModel, n => IgnoreUpdateProperties.Contains(n) == false);

            updateModel = await updateDataAccess.UpdateAsync(updateModel);
            await updateDataAccess.SaveChangesAsync();

            var afterUpdateModel = await updateDataAccessAfter.GetByIdAsync(insertModel.Id);

            Assert.IsNotNull(afterUpdateModel);
            Assert.AreEqual((expected - now).TotalSeconds, (afterUpdateModel.Date - now).TotalSeconds, 5.0);
        }
        [TestMethod]
        public async Task Create_WithValidNumber1_Succeed()
        {
            var now = DateTime.Now;
            var model = new Payment
            {
                Date = now,
                CreditCardNumber = "5012562444244093",
                Amount = 100m,
            };

            await Create_AccessModel_AndCheck(model);
        }
        [TestMethod]
        public async Task Create_WithValidNumber2_Succeed()
        {
            var now = DateTime.Now;
            var model = new Payment
            {
                Date = now,
                CreditCardNumber = "0136831385049882",
                Amount = 100m,
            };

            await Create_AccessModel_AndCheck(model);
        }
        [TestMethod]
        [ExpectedException(typeof(Logic.Modules.Exceptions.LogicException))]
        public async Task Create_WithInvalidNumberLength1_ThowException()
        {
            var now = DateTime.Now;
            var model = new Payment
            {
                Date = now,
                CreditCardNumber = "50125624442440932",
                Amount = 100m,
            };

            await Create_AccessModel_AndCheck(model);
        }
        [TestMethod]
        [ExpectedException(typeof(Logic.Modules.Exceptions.LogicException))]
        public async Task Create_WithInvalidNumberLength2_ThowException()
        {
            var now = DateTime.Now;
            var model = new Payment
            {
                Date = now,
                CreditCardNumber = "501256244424409",
                Amount = 100m,
            };

            await Create_AccessModel_AndCheck(model);
        }
        [TestMethod]
        [ExpectedException(typeof(Logic.Modules.Exceptions.LogicException))]
        public async Task Create_WithInvalidNumber1_ThowException()
        {
            var now = DateTime.Now;
            var model = new Payment
            {
                Date = now,
                CreditCardNumber = "5012562444244094",
                Amount = 100m,
            };

            await Create_AccessModel_AndCheck(model);
        }
        [TestMethod]
        [ExpectedException(typeof(Logic.Modules.Exceptions.LogicException))]
        public async Task Create_WithInvalidNumber2_ThowException()
        {
            var now = DateTime.Now;
            var model = new Payment
            {
                Date = now,
                CreditCardNumber = "5012562444244092",
                Amount = 100m,
            };

            await Create_AccessModel_AndCheck(model);
        }
        [TestMethod]
        public async Task Update_WithValidNumber_Succeed()
        {
            var now = DateTime.Now;
            var model = new Payment
            {
                Date = now,
                CreditCardNumber = "6443869235864592",
                Amount = 100m,
            };
            var changedmodel = new Payment
            {
                Date = now,
                CreditCardNumber = "0136831385049882",
                Amount = 100m,
            };
            IgnoreUpdateProperties.Add("Date");
            await CreateUpdate_AccessModel_AndCheck(model, changedmodel);
            IgnoreUpdateProperties.Remove("Date");
        }
    }
}
