deploy:
	git subtree push --prefix Assets/Plugins/A11YTK origin upm

deploy-force:
	git push origin `git subtree split --prefix Assets/Plugins/A11YTK master`:upm --force
