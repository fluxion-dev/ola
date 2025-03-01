using AutoFixture;
using AutoFixture.AutoNSubstitute;

namespace Ola.Tests;

public static class Some
{
    private static readonly IFixture Fixture = new Fixture()
        .Customize(new AutoNSubstituteCustomization {ConfigureMembers = true});
    
    public static T InstanceOf<T>() => Fixture.Create<T>();
    public static string String => Fixture.Create<string>();
}