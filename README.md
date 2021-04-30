# Next Release Version GitHub Action

GitHub Action for bumping Semantic Version Numbers based on branch name

## Inputs

| Input | Description |
|-|-|
| `owner` | **Required** The owner of the repository |
| `repo` | **Required** The repository name |
| `username` | **Required** The GitHub username |
| `token` | **Required** The GitHub token |
| `branch` | **Required** The current branch |
| `project` | The Project Type (Single or Multi) (Default: Single) |

## Outputs

| Output | Description |
|-|-|
| `next-version` | The next semantic version for the next release |

## Example usage

```yaml
- uses: wemogy/release-version-action@1.0.0
  id: release-version
  with:
    owner: 'wemogy'
    repo: 'hello-world'
    username: $GITHUB_ACTOR
    token: ${{ secrets.GITHUB_TOKEN }}
    branch: ${{ github.ref }}
    
- run: echo ${{ steps.release-version.outputs.next-version }}
```
