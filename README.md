🏋️ Sistema de Gerenciamento de Academia
👥 Integrantes

Vitor Dias dos Santos — RM: 565422

Enrico Delesporte — RM: 565760

Felipe Modesto — RM: 561810

📌 Domínio do Projeto

O domínio escolhido para o projeto foi Academia.

O sistema foi modelado para representar a estrutura de uma rede de academias, permitindo o gerenciamento de alunos, planos, fichas de treino, funcionários, unidades e demais elementos necessários para o funcionamento de uma academia moderna.

🧩 Entidades Modeladas

O modelo contém as seguintes entidades:

Plano

Aluno

Ficha de Treino

Aula Extra

Funcionário

Instrutor

Gerente

Unidade de Academia

Rede de Academia

Localização


📊 Modelo Entidade-Relacionamento (MER)


Ele apresenta:

Entidades do sistema

Atributos principais

Chaves primárias (PK)

Relacionamentos

Cardinalidades

Opcionalidades

📚 Descrição das Entidades
Plano

A entidade Plano armazena as informações dos planos oferecidos pela academia.

Ela contém dados como:

preço

tipo de plano

data de assinatura

data de renovação

fidelidade

status ativo

Esses planos podem ser associados aos alunos cadastrados.

Aluno

A entidade Aluno representa os clientes da academia.

São armazenadas informações como:

nome

CPF

e-mail

telefone

data de matrícula

status de atividade

Cada aluno está vinculado a um plano e pode possuir uma ficha de treino específica.

Ficha de Treino

A Ficha de Treino contém as informações relacionadas aos exercícios realizados pelos alunos.

Inclui dados como:

exercícios

número de repetições

séries

tipo de exercício

músculo alvo

observações do instrutor

Essa ficha é associada ao aluno para acompanhar sua rotina de treinamento.

Aula Extra

A entidade Aula Extra registra aulas adicionais oferecidas pela academia.

Exemplos:

Yoga

Funcional

Spinning

Ela possui informações como:

tipo de aula

horário

capacidade máxima de participantes

Funcionário

A entidade Funcionário representa os colaboradores da academia.

São armazenados dados como:

nome

CPF

e-mail

cargo

salário

data de contratação

status de atividade

Instrutor

O Instrutor é uma especialização da entidade Funcionário.

Ele representa os profissionais responsáveis por orientar os alunos nos treinos.

Possui também informações adicionais como:

registro profissional CREF

Gerente

A entidade Gerente também é uma especialização de Funcionário.

Ela representa os responsáveis pela gestão da academia.

Possui informações adicionais como:

comissão

período de liderança

área de responsabilidade

nível de gerência

Unidade da Academia

A entidade Unidade da Academia representa cada unidade física pertencente à rede de academias.

Ela possui dados como:

telefone

horário de funcionamento

status da unidade

Além disso, possui vínculos com:

gerente

funcionários

rede de academias

Rede de Academia

A entidade Rede de Academia armazena informações sobre a organização principal que administra as unidades.

Inclui dados como:

nome da rede

quantidade de unidades

CNPJ

data de fundação

Localização

A entidade Localização registra o endereço das unidades da academia.

Contém informações como:

estado

cidade

bairro

CEP

rua

número

Permitindo identificar a localização física de cada unidade.

🔗 Relacionamentos do Sistema
Plano — Aluno

Um Plano pode estar associado a vários alunos, enquanto cada Aluno possui apenas um plano ativo.

Cardinalidade:
Plano (1) → (N) Alunos

Aluno — Ficha de Treino

Cada Aluno possui uma ficha de treino que registra os exercícios e orientações definidas para ele.

Cardinalidade:
Aluno (1) → (1) Ficha de Treino

Funcionário — Instrutor / Gerente

Instrutores e Gerentes são especializações da entidade Funcionário.

Ou seja:

Todo Instrutor é um Funcionário

Todo Gerente é um Funcionário

Rede de Academia — Unidade da Academia

Uma Rede de Academia pode possuir várias unidades, enquanto cada Unidade pertence a apenas uma rede.

Cardinalidade:
Rede (1) → (N) Unidades

Unidade da Academia — Localização

Cada Unidade da Academia possui uma localização específica registrada no sistema.

Cardinalidade:
Unidade (1) → (1) Localização

Unidade da Academia — Funcionário

Os Funcionários trabalham em uma determinada Unidade da Academia.

Cardinalidade:
Unidade (1) → (N) Funcionários

Ficha de Treino — Aula Extra

As Fichas de Treino podem estar relacionadas às Aulas Extras disponíveis na academia, permitindo que os alunos participem de atividades adicionais além dos treinos convencionais.
