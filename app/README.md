# Estrutura do aplicativo web

## Login (Instrutor/Aluno)
- parte 1: Email
- parte 2: Senha (enviada por e-mail)

Obs.: Cadastro de novos alunos realizado pelo instrutor

## Instrutor

### Calendário
- seleção de datas
    - lista de turmas e horários do dia selecionado
        - página da turma selecionada
            - chamada realizada/pendente
            - lista de alunos e status
                - chamada realizada (presente/ausente)
                - chamada pendente (pendente)

### Chamada
- perfil do aluno
    - foto, nome e idade
    - opções: presente/ausente

### Turma
- lista de turmas
    - perfil da turma
        - nome, idade, dias da semana e horário
        - lista de alunos na turma
        - adicionar alunos
        - remover alunos
- adicionar nova turma
    - nome, modalidade, dias da semana e horário

### Aluno
- lista de alunos
    - perfil do aluno
        - foto, nome, idade e histórico de turmas
        - editar conta
        - desativar/desativar conta
- cadastrar aluno
    - Nome, data de nascimento, foto

### Relatório
- seleção de mês/ano
    - lista de turmas
        - gerar relatório
            - dia/mês, alunos, status (presente/ausente), total de aulas no mês (com frequência registrada) e total de frequências por aluno

## Aluno

### Perfil
- nome, foto, idade, histórico de turmas