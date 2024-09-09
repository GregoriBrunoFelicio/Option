namespace Option;

public abstract class Option<T>
{
    public static Option<T> Some(T value) => new SomeOption<T>(value);
    public static Option<T> None() => new NoneOption<T>();
    public abstract bool IsSome { get; }
    public abstract bool IsNone { get; }
    public abstract TR Match<TR>(Func<T, TR> foo, Func<TR> noFoo);

    private class SomeOption<TValue>(TValue value) : Option<TValue>
    {
        public override bool IsNone => false;
        public override bool IsSome => true;

        public override TR Match<TR>(Func<TValue, TR> foo, Func<TR> noFoo)
        {
            ArgumentNullException.ThrowIfNull(foo);
            return foo(value);
        }
    }
    
    private class NoneOption<TValue> : Option<TValue>
    {
        public override bool IsSome => false;
        public override bool IsNone => true;
        
        public override TR Match<TR>(Func<TValue, TR> foo, Func<TR> noFoo)
        {
            ArgumentNullException.ThrowIfNull(noFoo);
            return noFoo();
        }
    }
}