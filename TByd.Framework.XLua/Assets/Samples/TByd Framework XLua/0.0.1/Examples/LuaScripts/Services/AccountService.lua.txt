require("framework.System")

local ObservableObject = require("framework.ObservableObject")
local AsyncTask = require("framework.AsyncTask")
local Account = class("Account",ObservableObject)

---
-- 账号服务示例
-- 这只是一个示例，真实业务请使用异步方式，通过网络组件从服务器获取账号信息，缓存在本服务中
--@module AccountService
local M=class("AccountService")

function M:ctor()
	self.accounts = {}	
	local account = Account({username="test",password="test"})
	self.accounts[account.username] = account
end

M.register = async(function(self,account)
		self.accounts[account.username] = account
		return account
	end)

M.update = async(function(self,account)
		self.accounts[account.username] = account
		return account
	end)

M.login = async(function(self,username,password)
		local account = await(self:getAccount(username))		
		if account and account.password == password then
			return account
		end
		return nil
	end)

M.getAccount = async(function(self,username)
		return self.accounts[username]
	end)

return M