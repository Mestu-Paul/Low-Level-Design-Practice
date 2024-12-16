<h1>Foodkart:</h1>

<h3>Description:</h3>

Flipkart is starting a new online food ordering service. In this Service, users can order food from a restaurant which is serviceable in their area and the restaurant will deliver it. 

<h3>Features:</h3>

<ol>
<li>Restaurants can only serve one specialized dish.</li>
<li>Restaurants can serve in multiple areas.</li>
<li>At a time, users can order from one restaurant, and the quantity of food can be more than one.</li>
<li>Users should be able to rate any restaurant with or without comment.</li>
<li>Rating of a restaurant is the average rating given by all customers.</li>
</ol>

<h3>Requirements:</h3>
<ol>
    <li>Register a User:
        <ol>
            <li>register_user(user_details)<br>
            <i>user_details: name, gender, phoneNumber(unique) and pincode.</i></li>
        </ol>
    </li>
    <li>Users should be able to login, and all the operations will happen in the context of that user. If another user logged in, the previous user will automatically be logged out.
        <ol>
            <li>login_user(user_id): <i>this should set the context for all the next operation to be done by this user.</i></li>
        </ol>
    </li>
    <li>Register a restaurant in context of login user: 
        <ol>
            <li>Register_restaurant(resturant_name, list of serviceable pin-codes, food item name, food item price, initial quantity).</li>
        </ol>
    </li>
    <li>Restaurant owners should be able to increase the quantity of the food item.         
        <ol>
            <li>update_quantity(restaurant name, quantity to Add)</li>
        </ol>
    </li>
    <li>Users should be able to rate(1(Lowest)-5(Highest)) any restaurant with or without comment.
        <ol>
            <li>rate_restaurant(restaurant name, rating, comment)</li>
        </ol>
    </li>
    <li>User should be able to get list of all serviceable restaurant, food item name and price in descending order: 
        <ol>
            <li>
                show_restaurant(rating/price) Based on rating Based on Price NOTE: A restaurant is serviceable when it delivers to the user's pincode and has non-zero quantity of food item. </li>
        </ol>
    </li>
    <li>Place an order from any restaurant with any allowed quantity.
        <ol>
            <li>place_order(restaurant name, quantity)</li>
        </ol>
    </li>
    <li>Bonus: Order History of User: For a given user you should be able to fetch order history.</li>
</ol>

<h3>Other Details:</h3>
Do not use any database or NoSQL store, use in-memory store for now. Do not create any UI for the application. Write a driver class for demo purposes. Which will execute all the commands at one place in the code and test cases. Please prioritize code compilation, execution and completion. Please do not access the internet for anything EXCEPT syntax. You are free to use the language of your choice. All work should be your own. If found otherwise, you may be disqualified. Expectations: Code should be demoable (very important) Complete coding within the duration of 90 minutes. Code should be modular, with Object Oriented design. Maintain good separation of concerns. Code should be extensible. It should be easy to add/remove functionality without rewriting the entire codebase. Code should handle edge cases properly and fail gracefully. Code should be readable. Follow good coding practices: Use intuitive variable names, function names, class names etc. Indent code properly

<h3>Test cases:</h3>
<ul>
    <li>register_user(“Pralove”, “M”, “phoneNumber-1”, “HSR”)</li>
    <li>register_user(“Nitesh”, “M”, “phoneNumber-2”, “BTM”)</li>
    <li>register_user(“Vatsal”, “M”, “phoneNumber-3”, “BTM”)</li>
    <li>login_user(“phoneNumber-1”)</li>
    <li>register_restaurant(“Food Court-1”, “BTM/HSR”, “NI Thali”, 100, 5)<br>
        <i>NOTE: we will have 2 delimiters in input : ',' to specify separate fields & '/' to identify different pincodes.</i>
    </li>
    <li>register_restaurant(“Food Court-2”, “BTM”, “Burger”, 120, 3)</li>
    <li>login_user(“phoneNumber-2”) </li>
    <li>register_restaurant(“Food Court-3”, “HSR”, “SI Thali”, 150, 1)</li>
    <li>login_user(“phoneNumber-3”)</li>
    <li>show_restaurant(“price”)<br>
        <i>Output : Food Court-2, Burger Food Court-1, NI Thali</i>
    </li>
    <li>place_order(“Food Court-1”, 2) <br>
        <i>Output: Order Placed Successfully.</i>
    </li>
    <li>place_order(““Food Court-2”, 7) <br>
        <i>Output : Cannot place order</i>
    </li>
    <li>create_review(“Food Court-2”, 3, “Good Food”)</li>
    <li>create_review(“Food Court-1”, 5, “Nice Food”)</li>
    <li>show_restaurant(“rating”) <br>
        <i>Output : Food Court-1, NI Thali Food Court-2, Burger</i>
    </li>
    <li>login_user(“phoneNumber-1”) </li>
    <li>update_quantity(“Food Court-2”, 5) <br>
        <i>Output: Food Court-2, BTM, Burger - 8</i>
    </li>
    <li>update_location(“Food Court-2”, “BTM/HSR”) <br>
        <i>Output: Food Court-2, “BTM/HSR”, Burger - 8</i>
    </li>
</ul>

