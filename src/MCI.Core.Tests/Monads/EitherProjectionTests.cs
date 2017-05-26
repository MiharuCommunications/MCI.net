namespace Miharu.Core.Tests.Monads
{
    using System;
    using Xunit;

    public class EitherProjectionTests
    {
        private Either<string, int> left = new Left<string, int>("");

        private Either<string, int> right = new Right<string, int>(0);

        [Fact]
        public void IsTest()
        {
            Assert.True(this.left.Left.IsDefined);
            Assert.False(this.left.Left.IsEmpty);

            Assert.False(this.left.Right.IsDefined);
            Assert.True(this.left.Right.IsEmpty);

            Assert.False(this.right.Left.IsDefined);
            Assert.True(this.right.Left.IsEmpty);

            Assert.True(this.right.Right.IsDefined);
            Assert.False(this.right.Right.IsEmpty);
        }


        [Fact]
        public void GetTest()
        {
            Assert.Equal("", this.left.Left.Get());
            Assert.Throws<NullReferenceException>(() =>
            {
                this.left.Right.Get();
            });


            Assert.Throws<NullReferenceException>(() =>
            {
                this.right.Left.Get();
            });
            Assert.Equal(0, this.right.Right.Get());
        }


        [Fact]
        public void GetOrElseTest()
        {
            Assert.Equal("", this.left.Left.GetOrElse("fail"));

            Assert.Equal(1, this.left.Right.GetOrElse(1));


            Assert.Equal("success", this.right.Left.GetOrElse("success"));

            Assert.Equal(0, this.right.Right.GetOrElse(1));


            Assert.Equal("", this.left.Left.GetOrElse(() =>
            {
                throw new NotImplementedException();
            }));

            Assert.Equal(1, this.left.Right.GetOrElse(() =>
            {
                return 1;
            }));


            Assert.Equal("success", this.right.Left.GetOrElse(() =>
            {
                return "success";
            }));

            Assert.Equal(0, this.right.Right.GetOrElse(() =>
            {
                throw new NotImplementedException();
            }));
        }


        [Fact]
        public void ToOptionTests()
        {
            Assert.Equal("", this.left.Left.ToOption().Get());

            Assert.Throws<NullReferenceException>(() =>
            {
                this.left.Right.ToOption().Get();
            });


            Assert.Throws<NullReferenceException>(() =>
            {
                this.right.Left.ToOption().Get();
            });

            Assert.Equal(0, this.right.Right.ToOption().Get());
        }


        [Fact]
        public void SelectTests()
        {
            Assert.Equal("success", this.left.Left.Select(s => "success").Left.Get());

            Assert.Equal("", this.left.Right.Select<int>(i =>
            {
                throw new NotImplementedException();
            }).Left.Get());


            Assert.Equal(0, this.right.Left.Select<string>(s =>
            {
                throw new NotImplementedException();
            }).Right.Get());

            Assert.Equal(100, this.right.Right.Select(i => 100).Right.Get());
        }

        [Fact]
        public void SelectManyTests()
        {
            Assert.Equal("success", this.left.Left.SelectMany(s => new Left<string, int>("success")).Left.Get());

            Assert.Equal("", this.left.Right.SelectMany<int>(s =>
            {
                throw new NotImplementedException();
            }).Left.Get());


            Assert.Equal(0, this.right.Left.SelectMany<string>(i =>
            {
                throw new NotImplementedException();
            }).Right.Get());

            Assert.Equal(100, this.right.Right.SelectMany(i => new Right<string, int>(100)).Right.Get());
        }
    }
}
