FROM gradle:4.5.0-jdk8-alpine AS build-env
COPY ./ ./
RUN gradle build

FROM openjdk:8-jre
COPY --from=build-env /home/gradle/build/libs/myapp-all.jar /app/
WORKDIR /app/
CMD ["java","-jar","myapp-all.jar"]
