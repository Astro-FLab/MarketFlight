FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS installer-env
COPY . .
RUN dotnet run --project CodeCakeBuilder -nointeraction

FROM microsoft/aspnetcore
WORKDIR /app
COPY --from=installer-env release/ /app
