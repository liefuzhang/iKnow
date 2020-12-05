docker image build -t iknow . -f .\iknow\Dockerfile
docker tag iknow registry.heroku.com/iknow-netcore/web
docker push registry.heroku.com/iknow-netcore/web
heroku container:release web -a iknow-netcore
