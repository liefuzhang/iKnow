using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TransactionScope = System.Transactions.TransactionScope;

namespace iKnow.IntegrationTests {
    public class Isolated : Attribute, ITestAction {
        private TransactionScope _transactionScope;

        public void BeforeTest(ITest test) {
            _transactionScope = new TransactionScope();
        }

        public void AfterTest(ITest test) {
            _transactionScope.Dispose();
        }

        public ActionTargets Targets => ActionTargets.Test;
    }
}
