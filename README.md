# Solnet.Template

Template repository to easily bootstrap new program implementations using Solnet.

## Instructions

To quickly bootstrap a new project using the current "standard" project structure for program implementations using Solnet do the following:

- Click the `Use this template` button in the repository page.

- Choose the desired name for the project (i.e. `Solnet.Serum`, `Solnet.Mango`, `Solnet.Pyth`).

- Clone the newly created repository.

- Change every `Solnet.Template` occurrence in the repository to the desired project name:

    - Namespaces 
      - `Solnet.Template.Examples` already features the `IRunnableExample` and the code to get the classes which implement the interface in the assembly
    - Directories
    - SharedBuildProperties
    - `build.cake`
  
- Add a "logo" to the `assets` directory.

- Remove the `README.md` file and rename `PLACEHOLDER.md` to `README.md`.

- As you bootstrap the project, pipelines etc, change the badges accordingly.

- For generic guidelines on how you should bootstrap the project itself, in order to easily maintain the codebase of a program client implementation, see:
  - [Solnet.Programs](https://github.com/bmresearch/Solnet/tree/master/src/Solnet.Programs)
  - [Solnet.Serum](https://github.com/bmresearch/Solnet.Serum)

- ??????

- Profit.



