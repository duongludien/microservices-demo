using BalanceService.Domain.Interfaces;
using BalanceService.Domain.Models;
using GrpcBalance;
using GrpcWallet;
using Moq;
using WalletService.Api.Client;

[Binding]
public sealed class CreateForBudgetStepDefinitions
{
    private readonly ScenarioContext _scenarioContext;
    
    private readonly Mock<IBalanceRepository> _balanceRepositoryMock;
    private readonly Mock<IGrpcWalletService> _grpcWalletServiceMock;

    private BalanceService.Api.Grpc.BalanceService _balanceService;
    private CreateBalanceForBudgetRequest _request;
    private CreateBalanceForBudgetResponse _response;

    public CreateForBudgetStepDefinitions(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;

        _balanceRepositoryMock = new Mock<IBalanceRepository>();
        _balanceRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Balance>()))
            .Returns(new Balance(0, 0, 0));

        _grpcWalletServiceMock = new Mock<IGrpcWalletService>();
    }

    [Given(@"there is a budget which has id (.*)")]
    public void GivenThereIsABudgetWhichHasId(int p0)
    {
        _request = new CreateBalanceForBudgetRequest
        {
            BudgetId = p0
        };
    }

    [Given(@"there is an existing wallet named ""(.*)""")]
    public void WhenThereIsAnExistingWalletNamed(string p0)
    {
        _grpcWalletServiceMock.Setup(s => s.GetAllWalletsAsync())
            .ReturnsAsync(new GetAllWalletsResponse
            {
                Items =
                {
                    new WalletItemResponse
                    {
                        Id = 1,
                        Name = "Wallet 1"
                    }
                }
            });
    }
    
    [When(@"receive a request to create a balance for that budget")]
    public async Task WhenReceiveARequestToCreateABalanceForThatBudget()
    {
        _balanceService = new BalanceService.Api.Grpc.BalanceService(_balanceRepositoryMock.Object, _grpcWalletServiceMock.Object);
        _response = await _balanceService.CreateForBudget(_request, null);
    }
    
    [Then(@"a balance item should be created")]
    public void ThenABalanceItemShouldBeCreated()
    {
        
    }
}
