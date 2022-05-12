using Microsoft.VisualStudio.TestTools.UnitTesting;
using QTPayWithFunLight.Logic.Controllers;
using QTPayWithFunLight.Logic.Entities;
using System;
using System.Threading.Tasks;

namespace QTPayWithFunLight.Logic.UnitTest
{
    [TestClass]
    public class PaymentUnitTest : EntityUnitTest<Payment>
    {
        public override GenericController<Payment> CreateController()
        {
            return new PaymentsController();
        }
        [TestMethod]
        public async Task Create_DateIsNotSet_DateMustBeNow()
        {
            var now = DateTime.Now;
            var entity = new Payment
            {
                CreditCardNumber = "5012562444244093",
                Amount = 100m,
            };

            var expected = now;
            var actualEntity = await Create_Entity_AndCheck(entity);

            Assert.AreEqual((expected - now).TotalSeconds, (actualEntity.Date - now).TotalSeconds, 5.0);
        }
        [TestMethod]
        public async Task Create_DateIsSetWithNow_DateMustBeNow()
        {
            var now = DateTime.Now;
            var entity = new Payment
            {
                Date = now,
                CreditCardNumber = "5012562444244093",
                Amount = 100m,
            };

            var expected = now;
            var actualEntity = await Create_Entity_AndCheck(entity);

            Assert.AreEqual((expected - now).TotalSeconds, (actualEntity.Date - now).TotalSeconds, 5.0);
        }
        [TestMethod]
        public async Task Create_DateIsSetWithFuture_DateMustBeNow()
        {
            var now = DateTime.Now;
            var entity = new Payment
            {
                Date = now.AddHours(1),
                CreditCardNumber = "5012562444244093",
                Amount = 100m,
            };

            var expected = now;
            var actualEntity = await Create_Entity_AndCheck(entity);

            Assert.AreEqual((expected - now).TotalSeconds, (actualEntity.Date - now).TotalSeconds, 5.0);
        }
        [TestMethod]
        public async Task Update_DateIsChanged_DateMustBeUnchanged()
        {
            var now = DateTime.Now;
            var entity = new Payment
            {
                CreditCardNumber = "5012562444244093",
                Amount = 100m,
            };
            var changedEntity = new Payment
            {
                Date = now.AddHours(1),
                CreditCardNumber = "5012562444244093",
                Amount = 100m,
            };

            var expected = now;
            var insertEntity = await Create_Entity_AndCheck(entity);
            using var updateCtrl = CreateController();
            using var updateCtrlAfter = CreateController();

            var updateEntity = await updateCtrl.GetByIdAsync(insertEntity.Id);

            Assert.IsNotNull(updateEntity);
            updateEntity.CopyFrom(changedEntity, n => IgnoreUpdateProperties.Contains(n) == false);

            updateEntity = await updateCtrl.UpdateAsync(updateEntity);
            await updateCtrl.SaveChangesAsync();

            var afterUpdateEntity = await updateCtrlAfter.GetByIdAsync(insertEntity.Id);

            Assert.IsNotNull(afterUpdateEntity);
            Assert.AreEqual((expected - now).TotalSeconds, (afterUpdateEntity.Date - now).TotalSeconds, 5.0);
        }
        [TestMethod]
        public async Task Create_WithValidNumber1_Succeed()
        {
            var now = DateTime.Now;
            var entity = new Payment
            {
                Date = now,
                CreditCardNumber = "5012562444244093",
                Amount = 100m,
            };

            await Create_Entity_AndCheck(entity);
        }
        [TestMethod]
        public async Task Create_WithValidNumber2_Succeed()
        {
            var now = DateTime.Now;
            var entity = new Payment
            {
                Date = now,
                CreditCardNumber = "0136831385049882",
                Amount = 100m,
            };

            await Create_Entity_AndCheck(entity);
        }
        [TestMethod]
        [ExpectedException(typeof(Logic.Modules.Exceptions.LogicException))]
        public async Task Create_WithInvalidNumberLength1_ThowException()
        {
            var now = DateTime.Now;
            var entity = new Payment
            {
                Date = now,
                CreditCardNumber = "50125624442440932",
                Amount = 100m,
            };

            await Create_Entity_AndCheck(entity);
        }
        [TestMethod]
        [ExpectedException(typeof(Logic.Modules.Exceptions.LogicException))]
        public async Task Create_WithInvalidNumberLength2_ThowException()
        {
            var now = DateTime.Now;
            var entity = new Payment
            {
                Date = now,
                CreditCardNumber = "501256244424409",
                Amount = 100m,
            };

            await Create_Entity_AndCheck(entity);
        }
        [TestMethod]
        [ExpectedException(typeof(Logic.Modules.Exceptions.LogicException))]
        public async Task Create_WithInvalidNumber1_ThowException()
        {
            var now = DateTime.Now;
            var entity = new Payment
            {
                Date = now,
                CreditCardNumber = "5012562444244094",
                Amount = 100m,
            };

            await Create_Entity_AndCheck(entity);
        }
        [TestMethod]
        [ExpectedException(typeof(Logic.Modules.Exceptions.LogicException))]
        public async Task Create_WithInvalidNumber2_ThowException()
        {
            var now = DateTime.Now;
            var entity = new Payment
            {
                Date = now,
                CreditCardNumber = "5012562444244092",
                Amount = 100m,
            };

            await Create_Entity_AndCheck(entity);
        }
        [TestMethod]
        public async Task Update_WithValidNumber_Succeed()
        {
            var now = DateTime.Now;
            var entity = new Payment
            {
                Date = now,
                CreditCardNumber = "6443869235864592",
                Amount = 100m,
            };
            var changedEntity = new Payment
            {
                Date = now,
                CreditCardNumber = "0136831385049882",
                Amount = 100m,
            };
            IgnoreUpdateProperties.Add("Date");
            await CreateUpdate_Entity_AndCheck(entity, changedEntity);
            IgnoreUpdateProperties.Remove("Date");
        }
    }
}
