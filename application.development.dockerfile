FROM buildpack-deps:jessie-scm

# Install .NET CLI dependencies
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
        libc6 \
        libcurl3 \
        libgcc1 \
        libgssapi-krb5-2 \
        libicu52 \
        liblttng-ust0 \
        libssl1.0.0 \
        libstdc++6 \
        libunwind8 \
        libuuid1 \
        zlib1g \
        curl \
        gettext 
#    && rm -rf /var/lib/apt/lists/*


# Install .NET Core SDK
ENV DOTNET_SDK_VERSION 1.0.0-preview2.1-003155
ENV DOTNET_SDK_DOWNLOAD_URL https://dotnetcli.blob.core.windows.net/dotnet/preview/Binaries/$DOTNET_SDK_VERSION/dotnet-dev-debian-x64.$DOTNET_SDK_VERSION.tar.gz

RUN curl -SL $DOTNET_SDK_DOWNLOAD_URL --output dotnet.tar.gz \
    && mkdir -p /usr/share/dotnet \
    && tar -zxf dotnet.tar.gz -C /usr/share/dotnet \
    && rm dotnet.tar.gz \
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet
    
RUN curl -sSL -o dotnet.tar.gz https://go.microsoft.com/fwlink/?LinkID=827530 \
    && tar -zxf dotnet.tar.gz -C /usr/share/dotnet \ 
    && rm dotnet.tar.gz 


# Trigger the population of the local package cache
ENV NUGET_XMLDOC_MODE skip
RUN mkdir warmup \
    && cd warmup \
    && dotnet new \
    && cd .. \
    && rm -rf warmup \
    && rm -rf /tmp/NuGetScratch


COPY /src/Reichinger.Masterarbeit.PK-4-0 ./app
COPY /src/Reichinger.Masterarbeit.PK-4-0.Test ./tests

RUN (cd /tests && dotnet restore)

RUN (cd /app && dotnet restore)

EXPOSE 8000

WORKDIR /app

ENTRYPOINT ["dotnet", "run"]