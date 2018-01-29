FROM gradle:4.5.0-jdk8-alpine AS build-env

ARG http_proxy=
ARG https_proxy=
ARG no_proxy=
ARG JAVA_OPTS=

COPY ./ ./
RUN gradle build

FROM openjdk:8u151-jre-alpine3.7
COPY --from=build-env /home/gradle/build/libs/myapp-all.jar /app/
WORKDIR /app/
CMD ["java","-jar","myapp-all.jar"]
