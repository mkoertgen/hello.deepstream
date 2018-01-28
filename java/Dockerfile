FROM openjdk:8 AS build-env

ENV APP_HOME=/app/
RUN mkdir -p $APP_HOME/src/main/java
WORKDIR $APP_HOME

# download dependencies
COPY build.gradle gradlew $APP_HOME
COPY gradle $APP_HOME/gradle
RUN ./gradlew build --continue

COPY . .
RUN ./gradlew build

FROM openjdk:8-jre

COPY --from=build-env /app/build/libs/myapp-all.jar /app/
WORKDIR /app/

EXPOSE 8080

CMD ["java","-jar","myapp-all.jar"]
