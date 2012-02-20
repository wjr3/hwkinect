<?php if ( ! defined('BASEPATH')) exit('No direct script access allowed');

class Welcome extends CI_Controller {

	/**
	 * Index Page for this controller.
	 *
	 * Maps to the following URL
	 * 		http://example.com/index.php/welcome
	 *	- or -  
	 * 		http://example.com/index.php/welcome/index
	 *	- or -
	 * Since this controller is set as the default controller in 
	 * config/routes.php, it's displayed at http://example.com/
	 *
	 * So any other public methods not prefixed with an underscore will
	 * map to /index.php/welcome/<method_name>
	 * @see http://codeigniter.com/user_guide/general/urls.html
	 */
	public function index()
	{
		$this->show_login_page();
	}
	
	public function show_login_page($errors = '') {
		$data['main_content'] = 'login';
		$data['bodyData'] = $errors;
		$this->load->view('includes/template', $data);
	}
	
	public function validate_user() {
		$this->load->model('users_model');
		
		$this->form_validation->set_rules('username', 'Username', 'trim|required|min_length[4]');
		$this->form_validation->set_rules('password', 'Password', 'trim|required|min_length[4]|max_length[32]');
		
		if($this->form_validation->run() == FALSE) {
			$this->show_login_page();
			
		}
		else {
			$username = $this->input->post('username');
			$password = $this->input->post('password');
			
			$isUser = $this->users_model->validate($username, $password);
			if($isUser) {
				$userInfo = $this->users_model->get_user_info($username);
				$groupInfo = $this->users_model->get_user_group($userInfo['gid']);
				$data = array(
					'username' => $userInfo['name'],
					'group' => 'staff',//$groupInfo['name'],
					'is_logged_in' => TRUE
				);
				$this->session->set_userdata($data);
				redirect('submit_content/index');
			}
			else {
				$data = array(
					'errors' => 'The username and password supplied is incorrect. Please try again'
				);
				$this->show_login_page($data);
			}
		}
	}
	
	public function logout() {
		$this->session->unset_userdata('username');
		$this->session->unset_userdata('is_logged_in');
		redirect('submit_content/index');
	}
}

/* End of file welcome.php */
/* Location: ./application/controllers/welcome.php */