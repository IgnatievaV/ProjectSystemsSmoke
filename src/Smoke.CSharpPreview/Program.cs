// C# 15 memory safety (preview): declaring a pointer, taking an address and `fixed` no longer need an unsafe context.
int number = 42;
int* pointer = &number;

int[] numbers = [10, 20, 30];
fixed (int* first = numbers)
{
    // Dereferencing still requires an unsafe context.
    unsafe
    {
        Console.WriteLine($"*first + *pointer = {*first + *pointer}");
    }
}

Console.WriteLine($"Header.Signature = 0x{Header.Signature:X}");

class Header
{
    // A field initializer can't contain an unsafe block, but it can contain an unsafe expression.
    public static readonly int Signature = unsafe(ReadSignature());

    static unsafe int ReadSignature()
    {
        int rawValue = 0x1234;
        int* p = &rawValue;
        return *p;
    }
}
