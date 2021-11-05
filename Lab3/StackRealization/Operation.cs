using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackRealization
{
    public abstract class Operation
    {
        public abstract char Name { get; }
        public abstract int Priority { get; }
        public abstract int Evaluate(int[] nums);
    }
    public class Plus : Operation
    {
        public override char Name => '+';
        public override int Priority => 1;
        public override int Evaluate(int[] nums)
            => nums[1] + nums[0];

    }
    public class Minus : Operation
    {
        public override char Name => '-';
        public override int Priority => 1;
        public override int Evaluate(int[] nums)
            => nums[1] - nums[0];
    }
    public class Mult : Operation
    {
        public override char Name => '*';
        public override int Priority => 2;
        public override int Evaluate(int[] nums)
            => nums[1] * nums[0];
    }
    public class Div : Operation
    {
        public override char Name => '/';
        public override int Priority => 2;
        public override int Evaluate(int[] nums)
            => nums[1] / nums[0];
    }
    public class Degree : Operation
    {
        public override char Name => '^';
        public override int Priority => 3;
        public override int Evaluate(int[] nums)
            => (int)Math.Pow(nums[1], nums[0]);
    }
    public class LeftPair : Operation
    {
        public override char Name => '(';
        public override int Priority => 4;
        public override int Evaluate(int[] nums) { return default(int); }
    }
    public class RightPair : Operation
    {
        public override char Name => ')';
        public override int Priority => 4;
        public override int Evaluate(int[] nums) { return default(int); }
    }
}   
