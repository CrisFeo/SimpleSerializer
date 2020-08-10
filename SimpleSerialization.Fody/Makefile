DOTNET?='/mnt/c/Program Files/dotnet/dotnet.exe'
DOTNET_ARGS=-noLogo -clp:NoSummary -v n

.PHONY: clean
clean:
	$(DOTNET) clean $(DOTNET_ARGS)
	rm -rf bin obj

.PHONY: build
build:
	$(DOTNET) build $(DOTNET_ARGS)
