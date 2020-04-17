DOTNET_ARGS=-noLogo -clp:NoSummary -v n

.PHONY: clean
clean:
	cd SimpleSerialization && make clean
	cd SimpleSerialization.Fody && make clean
	cd SimpleSerialization.Test && make clean
	cd example-project && make clean

.PHONY: build
build:
	cd SimpleSerialization && make build
	cd SimpleSerialization.Fody && make build
	cd SimpleSerialization.Test && make build
	cd example-project && make build

.PHONY: test
test:
	cd SimpleSerialization.Test && make test

.PHONY: run-example
run-example:
	cd example-project && make run
