#if NET11_0_OR_GREATER
namespace Smoke.Library.Unions;

// C# 15: union types.
public record class Cat(string Name);
public record class Dog(string Name);
public record class Bird(string Name);

public union Pet(Cat, Dog, Bird);

public static class PetInfo
{
    // The switch is exhaustive over all case types - no default arm needed.
    public static string NameOf(Pet pet) => pet switch
    {
        Dog d => d.Name,
        Cat c => c.Name,
        Bird b => b.Name,
    };
}
#endif
