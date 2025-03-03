import { Browser, Builder, By, until, Key} from "selenium-webdriver";
import { expect } from "chai";

describe('Tests inside GameShop', function () {
    this.timeout(10000);
    let driver;
    before(async function () {
        driver = await new Builder().forBrowser(Browser.EDGE).build();
    });

    beforeEach(async function () {
        await driver.get("http://localhost:4200/home-page");
        await driver.manage().window().maximize();
    });

    it('Changing personal informations ',async function () {
        //Login to site
        await driver.sleep(1000);
        let loginText = await driver.findElement(By.xpath('//div[@class="text"]'),15000);
        await driver.wait(until.elementIsVisible(loginText),15000);
        await driver.wait(until.elementIsEnabled(loginText),15000);
        await loginText.click();

        await driver.sleep(1000);

        let usernameInput = await driver.findElement(By.xpath('//input[@placeholder="Username"]'),15000);
        await driver.wait(until.elementIsVisible(usernameInput),15000);
        await driver.wait(until.elementIsEnabled(usernameInput),15000);
        await usernameInput.click();
        await usernameInput.clear();
        await usernameInput.sendKeys("testacc12");

        await driver.sleep(500);

        let passwordInput = await driver.findElement(By.xpath('//input[@placeholder="Password"]'),15000);
        await driver.wait(until.elementIsVisible(passwordInput),15000);
        await driver.wait(until.elementIsEnabled(passwordInput),15000);
        await passwordInput.click();
        await passwordInput.clear();
        await passwordInput.sendKeys("adminadmin123A");

        await driver.sleep(500);

        let loginButton = await driver.findElement(By.xpath('//button[@class="login-button"]'),15000);
        await driver.wait(until.elementIsVisible(loginButton),15000);
        await driver.wait(until.elementIsEnabled(loginButton),15000);
        await loginButton.click();

        await driver.sleep(1000);

        //Opening profile page
        let usernameText = await driver.wait(until.elementLocated(By.xpath('//div[@class="text"]')),15000);
        await usernameText.click();

        await driver.sleep(1000);

        let changeInfoButton = await driver.wait(until.elementLocated(By.xpath('//button[text()="Change"]')),15000);
        await changeInfoButton.click();

        let modal = await driver.wait(until.elementLocated(By.id('exampleModalLive')),15000);
        await driver.wait(until.elementIsVisible(modal),15000);

        //Changing name
        let nameInput = await driver.findElement(By.xpath('//input[@placeholder="Name"]'),15000);
        await driver.wait(until.elementIsEnabled(nameInput),15000);
        await nameInput.click();
        await nameInput.clear();

        let randomName = generateRandomString(6);
        await nameInput.sendKeys(randomName);

        await driver.sleep(500);

        //Changing surname
        let surnameInput = await driver.findElement(By.xpath('//input[@placeholder="Surname"]'),15000);
        await driver.wait(until.elementIsEnabled(surnameInput),15000);
        await surnameInput.click();
        await surnameInput.clear();

        let randomSurname = generateRandomString(7);
        await surnameInput.sendKeys(randomSurname);

        await driver.sleep(500);

        //Confirming changes
        let nextButton = await driver.findElement(By.xpath('//button[@class="btn btn-primary"]'),15000);
        await nextButton.click();

        let passwordInputModal = await driver.wait(until.elementLocated(By.id('password')),15000);
        await passwordInputModal.click();
        await passwordInputModal.clear();
        await passwordInputModal.sendKeys("adminadmin123A");

        await driver.sleep(500);

        let passwordConfirmInputModal = await driver.wait(until.elementLocated(By.id('compassword')),15000);
        await passwordConfirmInputModal.click();
        await passwordConfirmInputModal.clear();
        await passwordConfirmInputModal.sendKeys("adminadmin123A");

        await driver.sleep(500);

        let updateButton = await driver.findElement(By.xpath('//button[@class="btn btn-primary"]'),15000);
        await nextButton.click();

        //Back to homepage
        let homeText= await driver.findElement(By.xpath('//p[text()="Home"]'));
        await driver.executeScript("arguments[0].click();", homeText);

        await driver.sleep(1000);
    });

    it('Adding games to cart', async function () {
        //Redirection to the Games page
        let gamesText = await driver.findElement(By.xpath('//p[text()="Games"]'),15000);
        await driver.wait(until.elementIsVisible(gamesText),15000);
        await driver.wait(until.elementIsEnabled(gamesText),15000);
        await driver.executeScript("arguments[0].click();",gamesText);

        await driver.sleep(1000);

        //Filtering games
        let minPrice = await driver.findElement(By.id('pocetna'),15000);
        await driver.wait(until.elementIsVisible(minPrice),15000);
        await driver.wait(until.elementIsEnabled(minPrice),15000);
        await minPrice.click();
        await minPrice.clear();
        await minPrice.sendKeys("30");

        await driver.sleep(500);

        let maxPrice = await driver.findElement(By.id('zavrsna'),15000);
        await driver.wait(until.elementIsVisible(maxPrice),15000);
        await driver.wait(until.elementIsEnabled(maxPrice),15000);
        await maxPrice.click();
        await maxPrice.clear();
        await maxPrice.sendKeys("60");

        await driver.sleep(500);

        let dropdownSort = await driver.findElement(By.id("sort"));
        await dropdownSort.click();
        let option = await driver.findElement(By.css('option[value="desc"]'));
        await option.click();

        await driver.sleep(500);

        await driver.get('http://localhost:4200/game-details/3');

        await driver.sleep(1000);
        //Adding to cart
        let addToCartButton = await driver.wait(until.elementLocated(By.xpath('//button[@class="button-manage"]')),15000);
        await addToCartButton.click();


        //Back to homepage
        await driver.get("http://localhost:4200/home-page");

        await driver.sleep(1000);
    });
    it('Search games by search bar', async function () {
        
        //Finding search bar
        let searchbar = await driver.wait(until.elementLocated(By.xpath('//div[@class="searchbar"]')),15000);
        await driver.wait(until.elementIsEnabled(searchbar),15000);
        let input = await driver.findElement(By.xpath('//input[@type="text"]'))
        await input.click();
        await driver.sleep(500);
        await input.clear();
        await input.sendKeys("The");
        await driver.sleep(1200);

        //Logout
        let userElement = await driver.wait(until.elementLocated(By.xpath("//div[@class='user']")), 10000);
        const actions = driver.actions({ async: true });
        await actions.move({ origin: userElement }).perform();
        let hiddenElement = await driver.wait(until.elementLocated(By.xpath('//button[@class="button-manage"]')),5000);
        await hiddenElement.click();

        await driver.sleep(1000);
    });

    it('Add/remove games', async function () {
        await driver.get("http://localhost:4200/home-page");
        this.timeout(30000);
        await driver.sleep(1000);
        //Login
        let loginText = await driver.findElement(By.xpath('//div[@class="text"]'),15000);
        await driver.wait(until.elementIsVisible(loginText),15000);
        await driver.wait(until.elementIsEnabled(loginText),15000);
        await loginText.click();
        
        await driver.sleep(1000);

        let usernameInput = await driver.findElement(By.xpath('//input[@placeholder="Username"]'),15000);
        await driver.wait(until.elementIsVisible(usernameInput),15000);
        await driver.wait(until.elementIsEnabled(usernameInput),15000);
        await usernameInput.click();
        await usernameInput.clear();
        await usernameInput.sendKeys("adminadmin");

        await driver.sleep(500);
        
        let passwordInput = await driver.findElement(By.xpath('//input[@placeholder="Password"]'),15000);
        await driver.wait(until.elementIsVisible(passwordInput),15000);
        await driver.wait(until.elementIsEnabled(passwordInput),15000);
        await passwordInput.click();
        await passwordInput.clear();
        await passwordInput.sendKeys("admintest123A");

        await driver.sleep(500);

        let loginButton = await driver.findElement(By.xpath('//button[@class="login-button"]'),15000);
        await driver.wait(until.elementIsVisible(loginButton),15000);
        await driver.wait(until.elementIsEnabled(loginButton),15000);
        await loginButton.click();

        await driver.sleep(1000);
       
        //Redirection to games page
        let gamesText = await driver.wait(until.elementLocated(By.xpath('//p[text()="Games"]')),15000);
        await driver.wait(until.elementIsEnabled(gamesText),15000);
        await driver.executeScript("arguments[0].click();",gamesText);

        await driver.sleep(500);
        //Adding new game
        let newGame = await driver.wait(until.elementLocated(By.xpath('//div[@class="game-add transition"]')),15000);
        await driver.wait(until.elementIsEnabled(newGame),15000);
        await driver.executeScript("arguments[0].click();",newGame);

        await driver.sleep(500);

        let modal= await driver.wait(until.elementLocated(By.id('exampleModalLive')),15000);
        await driver.wait(until.elementIsEnabled(modal),15000);

        let gameName = await driver.findElement(By.id('name'));
        await driver.wait(until.elementIsEnabled(gameName),15000);
        await gameName.click();
        await gameName.clear();
        let randomName = generateRandomString(7);
        await gameName.sendKeys(randomName);

        await driver.sleep(500);

        let gamePhoto = await driver.wait(until.elementLocated(By.id('photo')),15000);
        await gamePhoto.click();
        await gamePhoto.clear();
        await gamePhoto.sendKeys('https://th.bing.com/th/id/R.73274f24c297b68d172b1a688f4ba8e9?rik=nMM0DA%2fPg0Ji4Q&riu=http%3a%2f%2fwallpapercave.com%2fwp%2fwp1809626.jpg&ehk=rXNyXCC2bnEsSdbG8QQezRbVMFvUA6JkDV622U00H5M%3d&risl=&pid=ImgRaw&r=0');

        await driver.sleep(500);

        let gamePublisher = await driver.wait(until.elementLocated(By.id('publisher')),15000);
        await gamePublisher.click();
        await gamePublisher.clear();
        let randomGamePublisher = generateRandomString(8);
        await gamePublisher.sendKeys(randomGamePublisher);

        await driver.sleep(500);

        let gameDescription = await driver.wait(until.elementLocated(By.id('description')),15000);
        await gameDescription.click();
        await gameDescription.clear();
        let randomGameDescription = generateRandomString(10);
        await gameDescription.sendKeys(randomGameDescription);

        await driver.sleep(500);

        let gamePrice = await driver.wait(until.elementLocated(By.id('price')),15000);
        await gamePrice.click();
        await gamePrice.clear();
        let randomGamePrice = Math.floor((Math.random() * 100) + 1);
        await gamePrice.sendKeys(randomGamePrice);

        await driver.sleep(500);

        let gamePercentageDiscount = await driver.wait(until.elementLocated(By.id('percentageDiscount')),15000);
        await gamePercentageDiscount.click();
        await gamePercentageDiscount.clear();
        let randomGamePercentageDiscount = Math.floor((Math.random() * 100) + 1);
        await gamePercentageDiscount.sendKeys(randomGamePercentageDiscount);

        await driver.sleep(500);


        let releaseDate = await driver.findElement(By.id('releaseDate'));
        await driver.wait(until.elementIsEnabled(releaseDate),15000);
        await releaseDate.click();
        await releaseDate.clear();
        await releaseDate.sendKeys('02-11-2024');

        await driver.sleep(500);
        

        let actionDropDown = await driver.findElement(By.id('genre'));
        await actionDropDown.click();
        let randomNumber = Math.floor((Math.random()*10) + 1);
        let option = await driver.findElement(By.css(`select#genre option[value="${randomNumber}"]`));
        await option.click();
        await driver.findElement(By.tagName('body')).click();

        await driver.sleep(500);

        let saveChangesButton = await driver.wait(until.elementLocated(By.xpath('//button[@type="button" and contains(text(),"Save changes")]')),15000);
        await driver.wait(until.elementIsVisible(saveChangesButton), 15000);
        await driver.executeScript("arguments[0].click();",saveChangesButton);
        await driver.sleep(1000);
        //Removing game
        await driver.executeScript("window.scrollTo(0, document.body.scrollHeight);");
        let game = await driver.findElement(By.xpath(`//div[@class="game-item"]//div[@class="details"]//p[text()="${randomName}"]/ancestor::div[@class="game-item"]`));
        await driver.wait(until.elementIsVisible(game),15000);
        await driver.wait(until.elementIsEnabled(game),15000);
        let deleteButton = await game.findElement(By.xpath('.//div[@class="game-control"]//button[text()="Delete"]'));
        await driver.sleep(1000);
        await deleteButton.click();
        await driver.sleep(500);
        await driver.executeScript("window.scrollTo(0, document.body.scrollHeight);");

        //Back to home page
        await driver.sleep(1000);
        await driver.get("http://localhost:4200/home-page");
        await driver.sleep(1000);

        //Logout
        let userElement = await driver.wait(until.elementLocated(By.xpath("//div[@class='user']")), 10000);
        const actions = driver.actions({ async: true });
        await actions.move({ origin: userElement }).perform();
        let hiddenElement = await driver.wait(until.elementLocated(By.xpath('//button[@class="button-manage"]')),5000);
        await hiddenElement.click();

    });
    
     
    

    afterEach(async function () {
        
    });

    after(async function () {
        await driver.quit();
    });
});

function generateRandomString(length){
    const characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
    let result = '';
    const charactersLength = characters.length;

    for (let i = 0; i < length; i++) {
        result += characters.charAt(Math.floor(Math.random() * charactersLength));
    }

    return result;
}