FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS installer-env
WORKDIR /builddir
COPY . .
RUN dotnet run --project CodeCakeBuilder -nointeraction

FROM mcr.microsoft.com/dotnet/core/aspnet
WORKDIR /app
COPY --from=installer-env builddir/release/ /app
ENTRYPOINT ["dotnet", "MarketFlight.dll"]
