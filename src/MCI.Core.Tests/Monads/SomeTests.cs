﻿namespace Miharu.Core.Tests.Monads
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;


    public class SomeTests : IOptionTests<int>
    {
        public Option<int> Value { get; private set; }

        public SomeTests()
        {
            this.Value = Option<int>.Return(1);
        }

        [Fact]
        public void IsEmptyTest()
        {
            Assert.False(this.Value.IsEmpty);
        }

        [Fact]
        public void IsDefinedTest()
        {
            Assert.True(this.Value.IsDefined);
        }

        public void GetTest()
        {
            throw new NotImplementedException();
        }

        public void GetOrElseTest()
        {
            throw new NotImplementedException();
        }

        public void OrElseTest()
        {
            throw new NotImplementedException();
        }

        public void RecoverTest()
        {
            throw new NotImplementedException();
        }
    }
}