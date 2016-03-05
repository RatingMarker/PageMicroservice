FROM microsoft/aspnet
COPY ./src/PageMicroservice.Api /app
COPY ./sh /app
WORKDIR /app
RUN dnu restore

EXPOSE 5010
ENTRYPOINT ["dnx", "web", "--server.urls", "http://0.0.0.0:5010"]