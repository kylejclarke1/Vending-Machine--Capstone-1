using System.Collections.Generic;
using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CapstoneTests
{
    [TestClass]
    public class Tests
    {
        VendingMachine vendingMachine;

        [TestInitialize]
        public void Initialize()
        {
            vendingMachine = new VendingMachine();
            vendingMachine.ReadFile();
        }

        [TestMethod]
        public void FeedMoneyTest()
        {
            vendingMachine.FeedMoney(5);
            Assert.AreEqual(5.00M, vendingMachine.CurrentMoneyProvided);
            vendingMachine.FeedMoney(4);
            Assert.AreEqual(5.00M, vendingMachine.CurrentMoneyProvided);
            vendingMachine.FeedMoney(-10);
            Assert.AreEqual(5.00M, vendingMachine.CurrentMoneyProvided);
            Assert.AreEqual("Failure", vendingMachine.FeedMoney(3.00M));
            Assert.AreEqual("Success", vendingMachine.FeedMoney(10.00M));

        }

        [TestMethod]
        public void SelectProductTest()
        {
            vendingMachine.FeedMoney(10.00M);
            vendingMachine.SelectProduct("abcdefghijklmnopqrstuvwxyz");
            Assert.AreEqual(10.00M, vendingMachine.CurrentMoneyProvided);
            vendingMachine.SelectProduct("B1");
            Assert.AreEqual(8.20M, vendingMachine.CurrentMoneyProvided);
            vendingMachine.SelectProduct("B1");
           Assert.AreEqual("Munch Munch, Yum!",vendingMachine.SelectProduct("B1"));
            vendingMachine.SelectProduct("B1");
            vendingMachine.SelectProduct("B1");
            vendingMachine.SelectProduct("B1");
            vendingMachine.FeedMoney(1.00M);
            Assert.AreEqual("Sold Out", vendingMachine.SelectProduct("B1"));
            Assert.AreEqual(2.00M, vendingMachine.CurrentMoneyProvided);
            Assert.AreEqual("Insufficient Funds", vendingMachine.SelectProduct("A1"));
            Assert.AreEqual("Fake Item", vendingMachine.SelectProduct("E1"));      
        }

        [TestMethod]
        public void FinishTransactionTest()
        {
            vendingMachine.FeedMoney(1);
            vendingMachine.FinishTransaction();
            Assert.AreEqual(0.00M, vendingMachine.CurrentMoneyProvided);
            vendingMachine.FeedMoney(5);
            vendingMachine.SelectProduct("D1");
            Assert.AreEqual("You have 16 quarter(s), 1 dime(s), and 1 nickel(s) worth of change.", vendingMachine.FinishTransaction());          
        }

     
    }
}
