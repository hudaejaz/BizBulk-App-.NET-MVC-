**Project Title:B2B WholeSale Electronics Purchase Website**
**Introduction:**
The B2B Wholesale Platform facilitates transactions between businesses (buyers) and suppliers (sellers), allowing buyers to purchase electronics in bulk at wholesale prices. The platform is designed for two main roles:
•	Buyers (Businesses)
•	Sellers (Suppliers/Manufacturers)
**Functional Requirements:**
User Registration and Login
Product Search and Filtering
Quotation Request and Management
Shopping Cart and Checkout
Order Tracking
Payment Processing
**Homepage Layout:**
**Description**: The homepage serves as the entry point for users, providing easy navigation to various sections of the website. The design is clean and intuitive, utilizing Bootstrap elements for a responsive and modern look.
****Key Components:**
Navigation Bar:**
**Description:** A top navbar implemented using Bootstrap's navbar component.
**Features:** Includes links to the Home, Products, Quotations, Cart, and Account pages.
**Styling:** Custom CSS is used to match the website’s color scheme and branding.
Hero Section:
**Description:** A prominent section at the top of the homepage that highlights key features or promotions.
**Features: **Includes a large background image, headline, and call-to-action button.
**Product Categories:**
**Description:** A section displaying various product categories, each represented by a card.
**Features:** Each card includes an image, category name, and a link to the category page.
**Styling:** Uses Bootstrap’s card component with grid layout (row and col classes) for responsiveness.
**Featured Products:**
**Description:** Showcases a selection of featured products.
**Features:** Each product is displayed in a card format with an image, name, price, quick view and "Add to Cart" button.
**Styling:** Bootstrap’s card component and card-deck class are used for layout, with custom CSS for hover effects and transitions.

**Checkout Page:**
**Description:** The checkout page is designed to provide users with a seamless and intuitive process for completing their purchases. The layout is clean and organized, utilizing Bootstrap elements to ensure responsiveness and user-friendliness.
**Key Components:**
**Breadcrumb Navigation:**
**Description:** A breadcrumb trail at the top of the page to show the user’s progress in the checkout process.
**Features:** Includes links to previous steps (Cart, Shipping, Payment) and highlights the current step.
Styling: Uses Bootstrap’s breadcrumb component for a simple and clean navigation aid.
**Order Summary:**
**Description:** A section summarizing the items in the user's cart.
**Features:** Displays product images, names, quantities, and prices, along with the total amount.
**Styling:** Uses Bootstrap’s table component for a structured layout, with custom CSS for borders and spacing.
Shipping Information:
**Description:**A form where users enter their shipping details.
**Features:** Includes fields for name, address, city, state, zip code, and country.
**Styling:** Utilizes Bootstrap’s form-group and form-control classes for consistent form styling and layout.
**Payment Information:**
**Description:**A form for users to enter their payment details.
**Features:** Includes fields for card number, expiration date, CVV, and cardholder name.
**Styling:** Uses Bootstrap’s form-group and form-control classes for form fields, with custom CSS for secure input handling.
**Place Order Button:**
**Description:**A button to finalize the purchase.
**Features:** Prominently placed at the bottom of the page for easy access.
**Styling:** Styled using Bootstrap’s btn class with custom CSS to match the website’s color scheme.

**Common Page Layout Description:**
**Description:** Each product category page (Home Appliances, Accessories, Cameras) is designed to offer a consistent and user-friendly experience for browsing and purchasing products. The layout leverages Bootstrap elements for responsiveness and a modern look.
**Key Components:**
**Navigation Bar:**
**Description:** A fixed-top navbar for easy navigation across the website.
**Features:** Links to Home, Products, Quotations, Cart, and Account pages.
**Styling:** Implemented using Bootstrap’s navbar component with custom CSS for brand consistency.
**Banner Section:
Description:** A prominent banner at the top featuring category-specific highlights or offers.
**Features:** Large background image, headline, and call-to-action button.
**Product Grid:**
**Description:** A grid layout displaying products within the category.
**Features:** Each product is presented in a card format with an image, name, price, and "Add to Cart" button.
**Styling:** Uses Bootstrap’s card component and grid system (row and col) for a responsive design.
**Pagination and Filters:****
**Description:**** Pagination at the bottom and filters on the side to enhance product browsing.
**Features:** Numeric and next/previous buttons for pagination; filters for categories, brands, price range, and ratings.
**Styling:** Uses Bootstrap’s pagination, form-group, and form-check components, with custom CSS for usability.

**Backend Using MVC
Description**
The backend of the platform is implemented using ASP.NET Core MVC with Entity Framework Core for database interactions. Controllers manage the application logic and route requests to the appropriate views.
Controllers
HomeController
Manages homepage and general site navigation.
Fetches and displays featured products and categories.
CartController
Handles cart operations (add, update, remove items).
Displays cart summary and redirects to checkout.
UserController
Manages user registration, login, and account details.
Implements role-based access for buyers and sellers.
QuotationController
Facilitates quotation requests and responses.
Manages buyer-seller communication for bulk orders.
ProductController
Handles product listing, filtering, and detail views.
Entity Framework Core Models
User
Properties: UserId, Username, PasswordHash, Email, Role, etc.
Product
Properties: ProductId, Name, Description, Price, CategoryId, etc.
Quotation
Properties: QuotationId, BuyerId, SellerId, Details, Status, etc.
CartItem
Properties: CartItemId, UserId, ProductId, Quantity, etc.
Order
Properties: OrderId, UserId, TotalPrice, Status, etc.
**Technologies Used**
Frontend: HTML5, CSS3, Bootstrap 5
Backend: ASP.NET Core MVC, Entity Framework Core.
Database: SQL Server.

**Conclusion**
The B2B Wholesale Electronics Purchase Website provides a seamless and efficient platform for businesses to transact with suppliers. With a robust backend, responsive frontend, and user-friendly interface, it meets the dynamic needs of B2B wholesale transactions.




