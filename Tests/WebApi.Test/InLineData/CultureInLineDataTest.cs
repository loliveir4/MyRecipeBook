using System.Collections;

namespace WebApi.Test.InLineData;

public class CultureInLineDataTest : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { "en" };
        yield return new object[] { "pt-BR"};

    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
