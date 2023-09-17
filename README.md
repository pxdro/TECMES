# TECMES

# Usuário -> Entidade de Usuários da aplicação com propriedades de Nome, Email, Senha, Status e OrdemProducao 
# Regras:
# . Todo usuário é criado com OrdemProducao Inativo
# . OrdemProdução quando Ativa libera Adição e Edição das Ordens de Produção
# . Só podem haver usuários com emails distintos

# Produto -> Entidade Básica de negócios com propriedade Nome
# Regras:
# . Não pode excluir caso esteja sendo utilizada em alguma Ordem de Produção
# . Se alterar o Nome, atualiza em Ordem de Produção

# Ordem de Produção -> Entidade que herda 1 Produto e possui propriedades Quantidade e Liberado
# Regras:
# . Adição e Edição controlada por JWT
# . Depois de criada, não pode mais alterar a Quantidade (evitar erros envolvendo Produzidos de Produção)

# Produção -> Entidade que herda 1 Ordem de Produção e possui propriedade Produzido
# Regras:
# . Toda produção é criada com 0 Produzidas
# . Só pode criar e alterar caso Ordem de Produção esteja Liberada
# . Não pode criar produção de Ordem de Produção já utlizada
# . Não pode alterar Ordem de Produção utilizada
# . Número máximo de Produzidos é a Quantidade de Ordem de Produção relacionada

# Pedido -> Entidade que herda uma Produção e possui as propriedades Produto e Venda
# Regras:
# . Ao ser criado, excluir a Produção / Ordem de Produção referentes a venda
# . Após criado, É possível alterar somente o cliente
