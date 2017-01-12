using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class Registrations
    {
        [AssemblyInitialize]
        public static void InitializeAssembly(TestContext context)
        {
            var registrations = InstanceProvider.GetInstance<Registrations>();
            if (registrations != null) registrations.Intercept();
        }

        public virtual void Intercept()
        {
            // Nothing
        }
    }
}
