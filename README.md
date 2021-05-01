# Next Release Version

A GitHub Actions Task for determining the Semantic Version for the next release based on branch name and existing releases.

## Inputs

| Input | Description |
|-|-|
| `token` | **Required** A GitHub Access Token |
| `repo` | The repository name (Default: current repository) |
| `username` | The GitHub username (Default: current repository owner) |
| `branch` | The release branch (Default: current branch) |
| `project` | The Project Type (Single or Multi) (Default: Single) |

## Outputs

| Output | Description |
|-|-|
| `next-version` | The next semantic version for the next release |
| `folder` | The name of the folder for the branch |

## Example usage

```yaml
- uses: wemogy/next-release-version-action@0.1.6
  id: release-version
  with:    
    token: ${{ secrets.GITHUB_TOKEN }}
    project: 'Single'
    
- run: echo ${{ steps.release-version.outputs.next-version }}
```
