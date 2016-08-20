=======================================================================================================================================
												RELATION USER <-> ACCOUNTS <-> PROFILES
=======================================================================================================================================
	SecurityAccount links a Person to an Account, in this way same user could have an Client Account as well as a Store Owner Account
	Each user owns at least one account, so for instance an Store Owner will own 2 accounts the Client and the Store Owner

	The account owner could create other profiles based on the profile associated to him.
	Example, if we have an account of the type Store Owner
	- The account owner should be associated to a predefined "Store Profile" profile
	- The account owner could create more profiles based on the "Store Profile" profile
	- The account owner could invite/register other users to work with his account
	- The account owner could assign a profile to the users he invites

	At the same time the Store Owner in the sample above, will be the owner of all the profiles he creates
	This implies that profiles also have owners
	The Administrator will be the owner of all the main profiles:
	- "Store Profile"
	- "Client Profile"
	- "Provider Profile"
	- "Brand Profile"
=======================================================================================================================================
												HOW DIFFERENT USERS GET REGISTERED
=======================================================================================================================================
- Clients: 
		The public Sign-Up option will only allow to register users of the type Client.
        When the client is registered we need to create:
			- SecurityPerson : which contains information of the person it self
			- SecurityUser : its the user a SecurityPerson uses to login
			- SecurityAccountUser : links SecurityUsers to an Account.
			- SecurityAccount : will have a list of SecurityAccountUsers that can work with this Account
		The owner of the SecurityAccount just created would be the SecurityUser
        The default profile for clients is taking from the SecurityDefaultProfile
- Stores:
		The store will need to register as a client first
        Then will need to provide the email used for the registration to an Admin of the site in order to create the store.
        With the given email, the Site Admin will create:
			- Store : basic information regarding the Store and provided by the Owner in a formal way
			- SecurityAccount : A new account for the store. Assigns the already created user by the store owner as the owner of the account
			- SecurityAccountUser : Links the SecurityUser with the Account
			- SecurityAccountStore : Links the Store with the Account just created
        The default profile for Store owners is taking from SecurityDefaultProfile
- Registration by invitation:
        A Client or an Store owner could invite users to join into their accounts
        If the user is already registerd then we only need to create:
			- SecurityAccountUser : in order to link the user with that account
			- If the user is not registered, an email is sent so the user can register as client first
			- But this time it wont create an new SecurityAccount, instead will link the SecurityUser to the account that sent the invitation
=======================================================================================================================================
												HOW TO SIGN-IN
=======================================================================================================================================
For all users types:
- The user will enter the email and the password
- It is verified with the table SecurityUser
- Then we check in the SecurityAccountUser to see how many accounts does the user have
- If zero - we notify the user that his account was deleted from the previous account "SecurityUser.LastAccountUsed", and a continue button to create a new Account
- If only one - then checks the Account's status and if it is Active then sign-in, if not then follow the next step
- If more than one (or the account is Inactive), we list all the SecurityAccount grayout the onces that are inactive with an option to create a new account if it doesnt own one
- The user will pick an Account and that will be marked as default in the SecurityAccountUser table.
- Depending on the Account choosen, the web site will display the appropiated options

