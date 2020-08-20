
## Create a feature branch and merge to master
1. `git branch <new branch name>`: create a new branch
2. `git checkout <branch name>`: switch to branch "branch name"
3. `git pull origin master`: pull from master branch to whatever branch you are at
4.  if at this point, you have errors like "you have existing local changes, cannot pull because master branch will overwrite your local changes"
    1. `git add <add file that has changes>`: move changes from "workspace" to "index"
    2. `git commit -m "add comments"`: move changes from "index" to "local repository"
    3. `git pull origin master`: from "remote repository" to "workspace", you will see in terminal window saying you need to edit commit to see why this commit is necessary, you
        - press "i" to edit
        - write your comments
        - press "etc" (escape)
        - write ":wq" (write and quit)
        - then press enter
5. now you might see conflicts, and also will show "<branch name>/merging": 
    1. go the the file with conflicts
    2. save the change you want to make, delete all >>>, ===, <<<, and save the file
    3. `git add` and `git commit`
6. finally, `git pull origin master` will show no error
7. Make more changes, and `git add`, `git commit`, then finally you want to push changes from "local repository" to "remote repository", you send `git push origin HEAD`: a handy way to push the current branch to the same name on the remote
8. Go to GitLab, click "Merge Request", then "New Merge Request", and file you request, also "assign viewee", then submit "Merge Request"

For GitHub, step 8 is  `git checkout master`, `git merge <this branch name>`, `git push origin master`

## Delete branch after merge
Usually you want your feature branch to be deleted after merge. (You create a new feature branch for each feature), and you 
delete on GitLab GUI, but locally when you `git branch -a` it still shows up. Because there is a difference between a remote branch and a branch that exists in the remote repository. You need to update the list of remote branch using `git fetch --prune`: `--prune` will let you remove remote branches that no longer have a branch on the remote repository. (If you just do `git fetch` it will just update remote branches, but does not remove)
- `git branch -r`: to see an updated list of branches that really exist on the remote
- `git branch -d <branch name>`: delete local branches in Git
- `git push origin --delete <branch name on remote repository>`: delete remote branches in Git
- Then either one of 
    1. `git branch -r` you will not see this branch 
    2.  `git fetch --prune` and `git branch -a` then you will not see this branch

## Check available branch
- `git branch -a`: to see all branch available
- `git branch -r`: to see an updated list of branches that really exist on the remote

## Remote Branch vs Branch on Remote repository
- Remote branch (when you `git branch -a`, it shows under "remote/origin/<branch name>"): are local branches that map to branches of the remote repository. 
- branch `test` on remote repository will show as remote branch `/origin/test`

## Git pull
When you download other ppl's repo and make changes to it, also that person is updating the repo, how to git pull to update?

1. `git checkout -b casie`: create a new branch
2. `git add` and `git commit` your local changes to this branch casie
3. `git branch master` switch to master branch, then `git pull origin/master` to pull from master branch