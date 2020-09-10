`WORKDIR /app`: set work dir, this is the folder in the container where the app will be running from 
`COPY --from=build /my-app/build /usr/share/nginx/html`: copy from container's dir "my-app/build" to nginx dir "/usr/share/nginx/html"
`RUN rm /etc/nginx/conf.d/default.conf`: delete default nginx.conf file
`COPY nginx/nginx.conf /etc/nginx/conf.d`: copy our nginx.conf file (our nginx.conf file is stored in nginx folder under our current folder, and our current folder is all copied to my-app folder in container) to its required location in container /etc/nginx/conf.d/
`CMD ["nginx", "-g", "daemon off;"]`: start nginx server. We don't use `CMD ["npm", "start"]` because we are now in production mode, and we will use nginx as server rather than react built-in server. 
