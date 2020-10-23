FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5005

#ENV ASPNETCORE_Kestrel__Certificates__Default__Password="Test"
#ENV ASPNETCORE_Kestrel__Certificates__Default__Path="WsPc70.pfx"
#ENV ASPNETCORE_Kestrel__Endpoints__HttpsInlineCertFile__Certificate__Path="WsPc70.pfx"
#ENV ASPNETCORE_Kestrel__Endpoints__HttpsInlineCertFile__Certificate__Password="Test"
#ENV ASPNETCORE_Kestrel__Endpoints__HttpsInlineCertFile__Url="http://WsPc70:5005"
ENV ASPNETCORE_Kestrel__Endpoints__Http__Url="http://ws-pc-70:5005"
ENV ASPNETCORE_URLS=http://+:5005
#ENV ASPNETCORE_HTTPS_PORT=5005
ENV ConnectionStrings__TestDb="Data Source=172.20.0.2;Database=Identity;User Id=sa;Password=gjkjcfnbrA1$;"
ENV ServiceUrls__AuthorityApiEndpoint="http://Ws-Pc-70:5001"
ENV ServiceUrls__WebApiEndpoint="http://ws-pc-70:5007"
ENV ServiceUrls__DefaultRedirectUri="http://Ws-Pc-70:5005"
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Front.csproj", "./"]


RUN dotnet restore "./Front.csproj"

COPY . .

WORKDIR "/src/."

RUN dotnet build "Front.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Front.csproj" -c Release -o /app


#COPY ["*.pfx", "/app"]
#RUN update-ca-certificates

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Front.dll"]