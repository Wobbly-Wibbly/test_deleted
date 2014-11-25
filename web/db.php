<?php

class db{
	
	static $instance = null;
	
	protected $_connection = null;
	protected $_config = array(
		'username' => 'root'
		,'password' => ''
		,'host' => 'localhost'
		,'dbname' => 'words'
	);
	
	protected function __construct()
	{
		$this->_connection = new PDO(
				sprintf("mysql:host=%s;dbname=%s;charset=utf8", $this->_config['host'],$this->_config['dbname'])
				, $this->_config['username']
				, $this->_config['password']
				,array(PDO::MYSQL_ATTR_INIT_COMMAND => "SET NAMES utf8"));
	}
	
	public static function getInstance()
	{
		if(is_null(self::$instance))
		{
			self::$instance = new self();
		}
		return self::$instance;
	}
	
	public function __call($name, $arguments)
	{
		if(method_exists($this->_connection, $name))
        {
            return call_user_func_array(array($this->_connection, $name), $arguments);
        }
		throw new Exception('Method '.$name.' does not exist');
	}
}