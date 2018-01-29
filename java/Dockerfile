FROM gradle:4.5.0-jdk8-alpine AS build-env
RUN mkdir -p /app/src/main/java
WORKDIR /app
COPY ./ ./
RUN gradlew build

FROM openjdk:8-jre
COPY --from=build-env /app/build/libs/myapp-all.jar /app/
WORKDIR /app/
CMD ["java","-jar","myapp-all.jar"]
