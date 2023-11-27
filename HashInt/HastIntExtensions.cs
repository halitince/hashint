using HashidsNet;
using Microsoft.AspNetCore.Builder;

namespace Hi.Types;

public static class HastIntExtensions
{
    public static void UseHashInt(this IApplicationBuilder app, Hashids hashids)
    {
        HashInt.Hasher = hashids;
    }
}