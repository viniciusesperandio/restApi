Para desenvolver a ATIVIDADE 3, usei a ferramente swagger para a parte visual e foi desenvolvido
a APIRest em ASP.NET MVC Core, utilizando injeção de dependências 
e separando toda funcionalidade em services;


Caso de teste:
	- **Obs**: Para atualizar ou excluir algum registro, seja do candidato ou da empresa, precisa
			   de seu respectivo id(podendo verificar nos respectivos métodos de get), caso contrário
			   retornará BadRequest;
			   
	- Cadastrar Vagas/Tecnologias (uma por vez), no método RegisterCompany:
	
		{
		  "id": 0,
		  "technology": "C#",
		  "technologyValue": 10,
		  "availableJobOpportunity": true
		}
		{
		  "id": 0,
		  "technology": "JAVA",
		  "technologyValue": 15,
		  "availableJobOpportunity": true
		}
		{
		  "id": 0,
		  "technology": "PYTHON",
		  "technologyValue": 20,
		  "availableJobOpportunity": true
		}
		{
		  "id": 0,
		  "technology": "C++",
		  "technologyValue": 35,
		  "availableJobOpportunity": false
		}
	
	- Cadastrar Candidatos e suas respectivas tecnologias (uma por vez), no método RegisterCandidate:
	
		{
		  "id": 0,
		  "technologys": [
			"C#","C++"
		  ]
		}
		{
		  "id": 0,
		  "technologys": [
			"JAVA", "PYTHON"
		  ]
		}
		{
		  "id": 0,
		  "technologys": [
			"C#", "PYTHON"
		  ]
		}
		{
		  "id": 0,
		  "technologys": [
			"PYTHON", "C++"
		  ]
		}
	
	- Executar o método GetResult para receber o relatório ordenado por candidato;