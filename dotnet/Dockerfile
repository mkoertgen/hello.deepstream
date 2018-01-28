FROM microsoft/dotnet:2.0.5-sdk-2.1.4 AS build-env
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# build runtime image
FROM microsoft/dotnet:2.0.5-runtime

ARG GIT_COMMIT=unknown
ARG BUILD_DATE=unknown
ARG BUILD_VERSION=unknown

# TODO: deprecated, for legay ui frontend
MAINTAINER "Awesome Incremented <marcel.koertgen@gmail.com>"

# cf.: http://label-schema.org/rc1/
LABEL org.label-schema.build-date="${BUILD_DATE}" \
      org.label-schema.name="myapp" \
      org.label-schema.description="An example ASP MVC Web app" \
      org.label-schema.usage="https://github.com/mkoertgen/hello.deepstream/blob/master/README.md" \
      org.label-schema.url="https://github.com/mkoertgen/hello.deepstream" \
      org.label-schema.vcs-url="https://github.com/mkoertgen/hello.deepstream" \
      org.label-schema.vcs-ref="${GIT_COMMIT}" \
      org.label-schema.vendor="Awesome Incremented" \
      org.label-schema.version="${BUILD_VERSION}" \
      org.label-schema.schema-version="1.0" \
      org.label-schema.docker.cmd="docker run -d net"
# TODO: even more labels...

WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "ConsoleSample.dll"]