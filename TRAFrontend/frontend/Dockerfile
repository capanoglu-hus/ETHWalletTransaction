FROM node:18.13.0 AS base
WORKDIR /app
EXPOSE 3000

FROM node:18.13.0 AS install
WORKDIR /src
COPY  TRAFrontend/frontend/package-lock.json .
COPY TRAFrontend/frontend/package.json .
COPY TRAFrontend/frontend/public/.. .
RUN npm install
COPY . .


CMD ["npm" , "start"]