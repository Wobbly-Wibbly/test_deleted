<?php
include 'db.php';
include 'utils.php';
$db = db::getInstance();
//$statement = $db->prepare("INSERT INTO debug (email,data) VALUES ('".$_GET['email']."','".$_GET['data']."')");
//$statement->execute(array());
$email = 'N/A';
$data = 'N/A';
if(!empty($_POST))
{
	$email = $_POST['email'];
	$data = $_POST['data'];
}
if(!empty($_GET))
{
	$email = $_GET['email'];
	$data = $_GET['data'];
	
}
$statement = $db->prepare("INSERT INTO debug (email,data) VALUES ('".$email."','".$data."')");
$statement->execute(array());
utils::sendReply(1, "");