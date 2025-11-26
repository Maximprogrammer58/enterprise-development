using Bogus;
using System.Runtime.CompilerServices;
namespace AirlineApp.Generator.Generator;

/// <summary>
/// Extension class for the contract generator
/// </summary>
public static class TicketGeneratorExtensions
{
    /// <summary>
    /// Method for facilitating record generation
    /// </summary>
    /// <typeparam name="T">Parameter for the type of generated data</typeparam>
    /// <param name="faker">Data generator</param>
    public static Faker<T> WithRecord<T>(this Faker<T> faker) where T : class =>
        faker.CustomInstantiator(_ => RuntimeHelpers.GetUninitializedObject(typeof(T)) as T);

}
