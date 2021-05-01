# Next Version

A GitHub Actions Task for determining the Semantic Version for the next release based on branch name and existing releases.

This task expects, that you use [Semantic Versioning](https://semver.org/) and manage releases in `release/x.y` branches, where `x` is the major and `y` is the minor version. Once executed, the task fetches all existing Github Releases form the repository and finds the highest one for the branch it is executed on. Then it returns this version's patch number increased by one.

If your repository hosts more than one project and these projects have individual lifecycles, this task expects, that these projects are located in different folders on root level and that the releases are managed in `release/folder/x.y` branches, where `folder` matches the exact folder name of the project. The task then finds the latest matching GitHub Release with a `folder-x.y.z` tag and increases that number.

Check the examples below for more details.

## Inputs

| Input | Description |
|-|-|
| `token` | **Required** A GitHub Access Token |
| `repo` | The repository name (Default: current repository) |
| `username` | The GitHub username (Default: current repository owner) |
| `branch` | The release branch to check (Default: current branch) |
| `projects` | The amount of projects in this repo (Single or Multi) (Default: Single) |

## Outputs

| Output | Description |
|-|-|
| `next-version` | The next semantic version for the next release |
| `folder` | The name of the folder for the branch |

## Usage

```yaml
- uses: wemogy/next-release-version-action@0.1.6
  id: release-version
  with:
    token: ${{ secrets.GITHUB_TOKEN }}
    projects: 'Single'

- run: echo ${{ steps.release-version.outputs.next-version }}
```

## Examples

### Single project

Given your repo has GitHub Releases with the following tags:

- `1.0.0`
- `1.1.0`
- `1.1.1`
- `1.2.0`

and the following branches:

- `main`
- `release/1.0`
- `release/1.1`
- `release/1.2`

then a task with the configuration below generates the following outputs:

```yaml
- uses: wemogy/next-release-version-action@0.1.6
  id: release-version
  with:
    token: ${{ secrets.GITHUB_TOKEN }}
    branch: 'release/1.1'

- run: echo ${{ steps.release-version.outputs.next-version }} # Output: 1.1.2
- run: echo ${{ steps.release-version.outputs.folder }} # Output: <empty>
```

### Multiple projects

Given your repo contains multiple projects in the following folders:

- `project-a`
- `project-b`


GitHub Releases with the following tags:

- `project-a-1.0.0`
- `project-a-1.1.0`
- `project-b-1.0.0`

and the following branches:

- `main`
- `release/project-a/1.0`
- `release/project-a/1.1`
- `release/project-b/1.0`

then a task with the configuration below generates the following outputs:

```yaml
- uses: wemogy/next-release-version-action@0.1.6
  id: release-version
  with:
    token: ${{ secrets.GITHUB_TOKEN }}
    branch: 'release/project-a/1.1'
    projects: 'Multi'

- run: echo ${{ steps.release-version.outputs.next-version }} # Output: 1.1.1
- run: echo ${{ steps.release-version.outputs.folder }} # Output: project-a
```
