docker image build -t iknow . -f .\iknow\Dockerfile

docker run -it --rm -p 13131:80 iknow